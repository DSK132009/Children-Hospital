if (!(window.CSS && CSS.supports('scroll-margin-top:0'))) {
    var target = document.querySelector(':target');
    if (target) {
        var top = target.getBoundingClientRect().top;
        window.scrollTo(0, top + 200);
    }
}