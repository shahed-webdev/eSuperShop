﻿
let linkData = [
    {
        "LinkCategoryID": 1,
        "SN": 1,
        "Category": "Basic Setting",
        "IconClass": "fas fa-cog",
        "links": [{
            "LinkID": 1,
            "SN": 0,
            "Controller": "BasicSetting",
            "Action": "Region",
            "Title": "Region",
            "IconClass": null
        },
        {
            "LinkID": 2,
            "SN": 1,
            "Controller": "BasicSetting",
            "Action": "Area",
            "Title": "Area",
            "IconClass": null
        }, {
            "LinkID": 3,
            "SN": 2,
            "Controller": "BasicSetting",
            "Action": "CourierService",
            "Title": "Courier Service",
            "IconClass": null
        }, {
            "LinkID": 6,
            "SN": 2,
            "Controller": "BasicSetting",
            "Action": "HomeSlider",
            "Title": "Home Page Slider",
            "IconClass": null
        }]
    },
    {
        "LinkCategoryID": 1,
        "SN": 1,
        "Category": "Category",
        "IconClass": "fas fa-list-ul",
        "links": [{
            "LinkID": 3,
            "SN": 0,
            "Controller": "Category",
            "Action": "Index",
            "Title": "Category",
            "IconClass": null
        },
        {
            "LinkID": 1,
            "SN": 7,
            "Controller": "Category",
            "Action": "Placement",
            "Title": "Placement",
            "IconClass": null
        }]
    },
    {
        "LinkCategoryID": 2,
        "SN": 7,
        "Category": "Brand",
        "IconClass": "fas fa-tag",
        "links": [
            {
                "LinkID": 6,
                "SN": 2,
                "Controller": "Brand",
                "Action": "Add",
                "Title": "Add",
                "IconClass": null
            }, {
                "LinkID": 6,
                "SN": 2,
                "Controller": "Brand",
                "Action": "Assign",
                "Title": "Assign",
                "IconClass": null
            }]
    },
    {
        "LinkCategoryID": 2,
        "SN": 7,
        "Category": "Attribute",
        "IconClass": "fas fa-clipboard-list",
        "links": [
            {
                "LinkID": 6,
                "SN": 2,
                "Controller": "Attribute",
                "Action": "Add",
                "Title": "Add",
                "IconClass": null
            }, {
                "LinkID": 6,
                "SN": 2,
                "Controller": "Attribute",
                "Action": "Assign",
                "Title": "Assign",
                "IconClass": null
            }]
    },
    {
        "LinkCategoryID": 2,
        "SN": 7,
        "Category": "Specification",
        "IconClass": "fas fa-clipboard-list-check",
        "links": [
            {
                "LinkID": 6,
                "SN": 2,
                "Controller": "Specification",
                "Action": "Add",
                "Title": "Add",
                "IconClass": null
            }, {
                "LinkID": 6,
                "SN": 2,
                "Controller": "Specification",
                "Action": "Assign",
                "Title": "Assign",
                "IconClass": null
            }]
    },
    {
        "LinkCategoryID": 2,
        "SN": 7,
        "Category": "Seller",
        "IconClass": "fas fa-store",
        "links": [
            {
                "LinkID": 6,
                "SN": 2,
                "Controller": "Seller",
                "Action": "List",
                "Title": "List",
                "IconClass": null
            }]
    },
    {
        "LinkCategoryID": 2,
        "SN": 7,
        "Category": "Approval Info",
        "IconClass": "fas fa-user-lock",
        "links": [{
                "LinkID": 6,
                "SN": 2,
                "Controller": "ApprovalInfo",
                "Action": "PendingProfileInfo",
                "Title": "Profile Info",
                "IconClass": null
        }, {
            "LinkID": 6,
            "SN": 2,
            "Controller": "ApprovalInfo",
            "Action": "PendingSlider",
            "Title": "Image Slider",
            "IconClass": null
        }, {
            "LinkID": 6,
            "SN": 2,
            "Controller": "ApprovalInfo",
            "Action": "PendingCategory",
            "Title": "Category",
            "IconClass": null
        }, {
            "LinkID": 6,
            "SN": 2,
            "Controller": "ApprovalInfo",
                "Action": "PendingProductInfo",
                "Title": "Product Info",
            "IconClass": null
        }]
    },
    {
        "LinkCategoryID": 3,
        "SN": 7,
        "Category": "Order",
        "IconClass": "fas fa-luggage-cart",
        "links": [
            {
                "LinkID": 6,
                "SN": 2,
                "Controller": "Order",
                "Action": "OrderSettings",
                "Title": "Settings",
                "IconClass": null
            },
            {
                "LinkID": 6,
                "SN": 2,
                "Controller": "Order",
                "Action": "PendingList",
                "Title": "Pending List",
                "IconClass": null
            },
            {
                "LinkID": 6,
                "SN": 2,
                "Controller": "Order",
                "Action": "ConfirmedList",
                "Title": "Confirmed List",
                "IconClass": null
            }, {
                "LinkID": 6,
                "SN": 2,
                "Controller": "Order",
                "Action": "DeliveredList",
                "Title": "Delivered List",
                "IconClass": null
            }]
    }];

//selectors
const menuItem = document.getElementById("menuItem")

//functions

//get data from server
const getMenuData = function () {
    appendMenuDOM()
    setNavigation()
    //const url = '/Basic/GetSideMenu'
    //axios.get(url)
    //    .then(response => {
    //        appendMenuDOM(response.data)
    //        setNavigation()
    //    })
    //    .catch(err => console.log(err))
}

//create links
const createLinks = function (links) {
    const fragment = document.createDocumentFragment();
    links.forEach(link => {
        const anchor = document.createElement('a');
        anchor.classList.add('links')
        anchor.href = `/${link.Controller}/${link.Action}`
        anchor.textContent = link.Title

        const li = document.createElement('li');
        li.appendChild(anchor)

        fragment.appendChild(li)
    });

    return fragment
}

//create link li
const linkCategory = function (category, iconCss, links) {
    const span = document.createElement('span');
    span.className = iconCss

    const ico = document.createElement('i');
    ico.classList.add('fas', 'fa-caret-right')

    const strong = document.createElement('strong');
    strong.appendChild(span)
    strong.appendChild(ico)
    strong.appendChild(document.createTextNode(category))

    const ul = document.createElement('ul');
    ul.classList.add('sub-menu')
    ul.appendChild(links)

    const li = document.createElement('li');
    li.appendChild(strong)
    li.appendChild(ul)

    return li
}

//append link to DOM
const appendMenuDOM = function (/*linkData*/) {
    const fragment = document.createDocumentFragment();

    const span = document.createElement('span');
    span.className = 'fas fa-tachometer-alt'

    const anchor = document.createElement('a');
    anchor.classList.add('links')
    anchor.href = '/Dashboard/Index'
    anchor.textContent = 'Dashboard'

    const strong = document.createElement('strong');
    strong.appendChild(span)
    strong.appendChild(anchor)

    const li = document.createElement('li');
    li.appendChild(strong)

    fragment.appendChild(li)

    linkData.forEach(item => {
        const link = linkCategory(item.Category, item.IconClass, createLinks(item.links))
        fragment.appendChild(link)
    });

    menuItem.appendChild(fragment)
}

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
                const parentElement = link.parentElement.parentElement
                parentElement.previousElementSibling.classList.add("open")
                parentElement.classList.add("active")
            }

            link.classList.add('link-active')
        }
    })
}

//click on link
const linkCategoryClicked = function (evt) {
    const element = evt.target;
    if (element.nodeName === "STRONG") {
        if (element.nextElementSibling) {
            element.nextElementSibling.classList.toggle("active")
            element.classList.toggle("open")
        }
    }
}

//event listener
menuItem.addEventListener("click", linkCategoryClicked)

//on load
getMenuData()