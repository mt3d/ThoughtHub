window.mediumSidebar = (function () {

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

        // Same as Medium: overflow = sidebar height - viewport
        const overflow = sidebar.offsetHeight - viewportHeight;

        // Ignore short sidebars
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
                sidebar.style.top = (-overflow) + "px";
            }

            // stickyToTop -> notSticky
            if (state === "stickyToTop") {
                state = "notSticky";
                sidebar.style.position = "relative";
                sidebar.style.top = "0px";

                // IMPORTANT:
                // margin-top is computed ONCE and then frozen
                sidebar.style.marginTop = "0px";
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
                    overflow -
                    sidebar.offsetTop -
                    baseTopOffset -
                    upsellHeight + "px";
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