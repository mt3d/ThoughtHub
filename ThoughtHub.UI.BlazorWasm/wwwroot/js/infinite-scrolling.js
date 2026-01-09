/**
 * This function is called from Blazor via JavaScript interop.
 * 
 * lastItemIndicator is the <div> at the bottom of the feed.
 * 
 * componentInstance is a reference to the Blazor component, passed from C#
 * that will receive callbacks.
 */
export function initialize(lastItemIndicator, componentInstance) {
    // controls when the observer triggers
    const options = {
        // the element whose scrolling will be observed
        root: findClosestScrollContainer(lastItemIndicator),
        // extra space to trigger earlier/later (here it means exact visibility)
        rootMargin: '0px',
        // fire as soon as any pixel becomes visible
        threshold: 0,
    };

    const observer = new IntersectionObserver(async (entries) => {
        // When the lastItemIndicator element is visible => invoke the C# method `LoadMoreItems`
        for (const entry of entries) {
            if (entry.isIntersecting) {
                // temporarily stops observing the element (unobserve), so it doesn’t
                // trigger multiple times while items are loading.
                observer.unobserve(lastItemIndicator);

                await componentInstance.invokeMethodAsync("LoadMoreItems");
            }
        }
    }, options);

    observer.observe(lastItemIndicator);

    // Allow to cleanup resources when the Razor component is removed from the page
    return {
        // disconnects the observer (when the component is destroyed).
        dispose: () => dispose(observer),

        // TODO: Explain more.
        // re-observes the "new" last item after the list has grown.
        // For example, after loading more items, you need to tell the JS code “the new last item is here”.
        onNewItems: () => {
            observer.unobserve(lastItemIndicator);
            observer.observe(lastItemIndicator);
        },
    };
}

// Cleanup resources
function dispose(observer) {
    observer.disconnect();
}


/**
 * Typically, you'll want to watch for intersection changes with regard to the
 * target element's closest scrollable ancestor, or, if the target element isn't
 * a descendant of a scrollable element, the device's viewport. 
 */
function findClosestScrollContainer(element) {
    while (element) {
        const style = getComputedStyle(element);

        // finds an ancestor element that has scrolling enabled (overflow-y not “visible”).
        // That element becomes the root for the IntersectionObserver — so the
        // observer triggers relative to that container rather than the whole window.
        if (style.overflowY !== 'visible') {
            return element;
        }
        element = element.parentElement;
    }
    return null;
}