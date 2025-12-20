window.stickySidebar = (function () {

    // Internal state (equivalent to useRef)
    let lastScrollY = 0;
    let sidebar = null;
    let inner = null;

    // "stickyToTop" | "notSticky" | "stickyToBottom"
    let state = "stickyToTop";

    let fullNavbarHeight = 0;
    let upsellHeight = 0;
    let baseTopOffset = 0;

    function onScroll() {
        if (!sidebar) return;

        const scrollingDown = window.scrollY > lastScrollY;
        lastScrollY = window.scrollY;

        const viewportHeight = window.innerHeight;

        // If sidebar fits inside viewport, do nothing.
        // We do NOT use sticky bottom unless the sidebar is taller than the screen.
        // This prevents jitter, pointless state transitions, and broken layouts on small sidebars.
        const overflow = sidebar.offsetHeight - viewportHeight;
        if (overflow <= 20) return;

        if (scrollingDown) {

            // ---- SCROLLING DOWN ----

            // notSticky -> stickyToBottom
            if (
                state === "notSticky" &&
                window.scrollY >=
                sidebar.offsetTop +
                overflow +
                baseTopOffset +
                upsellHeight
            ) {
                state = "stickyToBottom";
                sidebar.style.position = "sticky";

                // pushes the element up so its bottom aligns with viewport bottom
                sidebar.style.top = (-overflow) + "px";
            }

            // stickyToTop -> notSticky
            if (state === "stickyToTop") {
                state = "notSticky";
                sidebar.style.position = "relative";
                sidebar.style.top = "0px";

                // IMPORTANT:
                // margin-top is computed ONCE and then frozen
                // margin-top is set so it does not jump.
                // The initial margin-top exists to prevent a visual jump when the
                // sidebar stops being sticky and re - enters normal document flow.
                sidebar.style.marginTop = "0px";

                // lastScrollY - sidebar.offsetTop: How far the page has scrolled past the sidebar’s original position.
                sidebar.style.marginTop =
                    Math.max(
                        lastScrollY - sidebar.offsetTop,
                        0
                    ) + "px";
            }

        } else {

            // ---- SCROLLING UP ----

            // notSticky -> stickyToTop
            if (
                state === "notSticky" &&
                window.scrollY <= sidebar.offsetTop
            ) {
                state = "stickyToTop";
                sidebar.style.position = "sticky";
                sidebar.style.top = (baseTopOffset + upsellHeight) + "px";
                sidebar.style.marginTop = "0px";
            }

            // stickyToBottom -> notSticky
            if (state === "stickyToBottom") {
                state = "notSticky";
                sidebar.style.position = "relative";
                sidebar.style.top = "0px";

                // Freeze margin-top once when leaving bottom sticky
                sidebar.style.marginTop = "0px";
                sidebar.style.marginTop =
                    lastScrollY -
                    overflow - // How much taller the sidebar is than the viewport
                    sidebar.offsetTop - // Sidebar’s natural top position in the document
                    baseTopOffset -
                    upsellHeight + "px"; // Extra dynamic header space Medium adds
            }
        }
    }

    return {
        init: function (sidebarId, innerId, options) {

            sidebar = document.getElementById(sidebarId);
            inner = document.getElementById(innerId);

            if (!sidebar || !inner) return;

            fullNavbarHeight = options.fullNavbarHeight;
            upsellHeight = options.upsellHeight;
            baseTopOffset = options.baseTopOffset;

            // Initial sticky state (matches Medium render)
            sidebar.style.position = "sticky";
            sidebar.style.top = fullNavbarHeight + "px";

            inner.style.minHeight =
                options.minHeight ??
                `calc(100vh - ${fullNavbarHeight}px)`;

            lastScrollY = window.scrollY;
            state = "stickyToTop";

            window.addEventListener("scroll", onScroll);
        },

        dispose: function () {
            window.removeEventListener("scroll", onScroll);
            sidebar = null;
            inner = null;
        }
    };
})();