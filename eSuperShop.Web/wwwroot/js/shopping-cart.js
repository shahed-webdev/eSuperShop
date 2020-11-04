
var shoppingCart = (function () {
   let cart = [];

    // Constructor
    function Item(name, price, count) {
        this.name = name;
        this.price = price;
        this.count = count;
    }

    // Save cart
    function saveCart() {
        sessionStorage.setItem('shoppingCart', JSON.stringify(cart));
    }

    // Load cart
    function loadCart() {
        cart = JSON.parse(sessionStorage.getItem('shoppingCart'));
    }

    if (sessionStorage.getItem("shoppingCart") != null) {
        loadCart();
    }


    // =============================
    // Public methods and properties
    // =============================
    const obj = {};

    // Add to cart
    obj.addItemToCart = function (name, price, count) {
        var item;
        for (item in cart) {
            if (cart.hasOwnProperty(item)) {
                if (cart[item].name === name) {
                    cart[item].count++;
                    saveCart();
                    return;
                }
            }
        }

        item = new Item(name, price, count);
        cart.push(item);
        saveCart();
    }

    // Set count from item
    obj.setCountForItem = function (name, count) {
        for (let i in cart) {
            if (cart.hasOwnProperty(i)) {
                if (cart[i].name === name) {
                    cart[i].count = count;
                    break;
                }
            }
        }
    };

    // Remove item from cart
    obj.removeItemFromCart = function (name) {
        for (let item in cart) {
            if (cart.hasOwnProperty(item)) {
                if (cart[item].name === name) {
                    cart[item].count--;
                    if (cart[item].count === 0) {
                        cart.splice(item, 1);
                    }
                    break;
                }
            }
        }
        saveCart();
    }

    // Remove all items from cart
    obj.removeItemFromCartAll = function (name) {
        for (let item in cart) {
            if (cart.hasOwnProperty(item)) {
                if (cart[item].name === name) {
                    cart.splice(item, 1);
                    break;
                }
            }
        }
        saveCart();
    }

    // Clear cart
    obj.clearCart = function () {
        cart = [];
        saveCart();
    }

    // Count cart 
    obj.totalCount = function () {
        var totalCount = 0;
        for (let item in cart) {
            if (cart.hasOwnProperty(item)) {
                totalCount += cart[item].count;
            }
        }
        return totalCount;
    }

    // Total cart
    obj.totalCart = function () {
        let totalCart = 0;
        for (let item in cart) {
            if (cart.hasOwnProperty(item)) {
                totalCart += cart[item].price * cart[item].count;
            }
        }
        return Number(totalCart.toFixed(2));
    }

    // List cart
    obj.listCart = function () {
        const cartCopy = [];
        for (let i in cart) {
            if (cart.hasOwnProperty(i)) {
               const item = cart[i];
                const itemCopy = {};
                for (let p in item) {
                    if (item.hasOwnProperty(p)) {
                        itemCopy[p] = item[p];
                    }
                }

                itemCopy.total = Number(item.price * item.count).toFixed(2);
                cartCopy.push(itemCopy)
            }
        }
        return cartCopy;
    }

    // cart : Array
    // Item : Object/Class
    // addItemToCart : Function
    // removeItemFromCart : Function
    // removeItemFromCartAll : Function
    // clearCart : Function
    // countCart : Function
    // totalCart : Function
    // listCart : Function
    // saveCart : Function
    // loadCart : Function
    return obj;
})();


// *****************************************
// Triggers / Events
// ***************************************** 
// Add item
$('.add-to-cart').click(function (event) {
    event.preventDefault();

    const name = $(this).data('name');
    const price = Number($(this).data('price'));

    shoppingCart.addItemToCart(name, price, 1);
    displayCart();
});

// Clear items
$('.clear-cart').click(function () {
    shoppingCart.clearCart();
    displayCart();
});


function displayCart() {
    const cartArray = shoppingCart.listCart();
    var output = "";
    for (let i in cartArray) {
        if (cartArray.hasOwnProperty(i)) {
            output +=`<tr>
                    <td><strong>${cartArray[i].name}</strong></td>
                    <td>৳${cartArray[i].price}</td>
                    <td class="text-center text-md-left">
                        <span class="qty">${cartArray[i].count}</span>
                        <div class="btn-group radio-group ml-2" data-toggle="buttons">
                            <label class="btn btn-sm btn-dark btn-rounded minus-item" data-name="${cartArray[i].name}">
                                <input type="radio" name="radio-quantity">&mdash;
                            </label>
                            <label class="btn btn-sm btn-dark btn-rounded plus-item" data-name="${cartArray[i].name}">
                                <input type="radio" name="radio-quantity">+
                            </label>
                        </div>
                    </td>
                    <td class="font-weight-bold"><strong>৳${cartArray[i].total}</strong></td>
                    <td class="text-right">
                        <button data-name="${cartArray[i].name}" type="button" class="btn btn-sm btn-danger delete-item" data-toggle="tooltip" data-placement="top" title="Remove item">X</button>
                    </td>
                </tr>`
        }
    }

    output +=`<tr>
                <td></td>
                <td><h4 class="mt-2"><strong>Total</strong></h4></td>
                <td class="text-right"><h4 class="mt-2"><strong class="total-cart">0</strong></h4></td>
                <td colspan="2" class="text-right">
                    <button type="button" class="btn btn-danger darken-2 btn-rounded px-4">
                        Complete purchase <i class="fas fa-angle-right right"></i>
                    </button>
                </td>
             </tr>`


    $('.show-cart').html(output);
    $('.total-cart').html(shoppingCart.totalCart());
    $('.total-count').html(shoppingCart.totalCount());
}

// Delete item button
$('.show-cart').on("click", ".delete-item", function (event) {
    const name = $(this).data('name');

    shoppingCart.removeItemFromCartAll(name);
    displayCart();
})


// -1
$('.show-cart').on("click", ".minus-item", function (event) {
    const name = $(this).data('name');
    shoppingCart.removeItemFromCart(name);

    displayCart();
})

// +1
$('.show-cart').on("click", ".plus-item", function (event) {
    const name = $(this).data('name');

    shoppingCart.addItemToCart(name);
    displayCart();
})

// Item count input
//$('.show-cart').on("change", ".item-count", function (event) {
//    const name = $(this).data('name');
//    const count = Number($(this).val());

//    shoppingCart.setCountForItem(name, count);
//    displayCart();
//});


displayCart();
