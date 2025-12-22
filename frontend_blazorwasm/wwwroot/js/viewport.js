window.viewport = {
    getWidth: () => window.innerWidth,

    // A reference to our viewport service object. When the size of the viewport
    // changes, the handler will call a method on the reference.
    subscribe: (dotNetRef) => {
        const handler = () => {
            dotNetRef.invokeMethodAsync("OnViewportChanged", window.innerWidth);
        };

        window.addEventListener("resize", handler);

        return {
            unsubscribe: () => {
                window.removeEventListener("resize", handler);
            }
        };
    }
}