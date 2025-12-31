if ("scrollRestoration" in history) {
    history.scrollRestoration = "manual";
}

window.scrollToTop = () => {
    document.documentElement.scrollTop = 0;
};
