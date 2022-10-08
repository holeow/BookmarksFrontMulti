var delay = 250, // delay between calls
    throttled = false;

function getDimensions() {
    //w.innerHTML = window.innerWidth;
    //h.innerHTML = window.innerHeight;
    DotNet.invokeMethodAsync('BookmarksFront', 'RaiseWindowSizeChanged', window.innerWidth, window.innerHeight);
    // Call dotnet static function here !
}

var getWindowWidth = function () {
    return window.innerWidth;
}
var getWindowHeight = function () {
    return window.innerHeight;
}
window.addEventListener('resize', function () {
    // only run if we're not throttled
    if (!throttled) {
        // actual callback action
        
        // we're throttled!
        throttled = true;
        // set a timeout to un-throttle
        setTimeout(function () {
            throttled = false;
            getDimensions();
        }, delay);
    }
});

