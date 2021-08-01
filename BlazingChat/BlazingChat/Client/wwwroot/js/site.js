export function setScroll() {
    //let's fix scroll here
    var divMessageContainerBase = document.getElementById('divMessageContainerBase');
    if (divMessageContainerBase != null) {
        divMessageContainerBase.scrollTop = divMessageContainerBase.scrollHeight;
    }
}