﻿
//selectors
const menuItem = document.getElementById("menuItem");

//event listener
menuItem.addEventListener("click", function (evt) {
    const element = evt.target;
    if (element.nodeName === "STRONG") {
        if (element.nextElementSibling) {
            element.nextElementSibling.classList.toggle("active")
            element.classList.toggle("open")
        }
    }
});

//active current url
function setNavigation() {
    const links = menuItem.querySelectorAll(".links")
    let path = window.location.pathname

    path = path.replace(/\/$/, "")
    path = decodeURIComponent(path)

    links.forEach(link => {
        const href = link.getAttribute('href')

        if (path === href) {
            if (link.parentElement.nodeName !== "STRONG") {
                const prentElement = link.parentElement.parentElement
                prentElement.previousElementSibling.classList.add("open")
                prentElement.classList.add("active")
            }

            link.classList.add('link-active')
        }
    })
}


//on load
setNavigation();