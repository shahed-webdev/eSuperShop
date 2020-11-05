
const shoppingCart = (function () {
   let cart = [];

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
    obj.addItemToCart = function (product) {
        for (let item in cart) {
            if (cart[item].ProductQuantitySetId === product.ProductQuantitySetId) {
                cart[item].Quantity = product.Quantity;
                saveCart();
                return;
            }
        }

        cart.push(product);
        saveCart();
    }

    // input quantity
    obj.inputQuantity = function (id, quantity) {
        for (let item in cart) {
            if (cart[item].ProductQuantitySetId === id) {
                cart[item].Quantity = quantity;
                break;
            }
        }
    };

    // increase quantity
    obj.increaseQuantity = function (id) {
        for (let item in cart) {
            if (cart[item].ProductQuantitySetId === id) {
                cart[item].Quantity++;
                saveCart();
                return;
            }
        }
    }

    // decrease quantity
    obj.decreaseQuantity = function (id) {
        for (let item in cart) {
            if (cart[item].ProductQuantitySetId === id) {
                cart[item].Quantity--;
                if (cart[item].Quantity === 0) {
                    cart.splice(item, 1);
                }
                break;
            }
        }

        saveCart();
    }

    // Remove product from cart
    obj.removeProduct = function (id) {
        for (let item in cart) {
            if (cart[item].ProductQuantitySetId === id) {
                cart.splice(item, 1);
                break;
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
                totalCount += cart[item].Quantity;
            }
        }
        return totalCount;
    }

    // Total cart
    obj.totalCart = function () {
        let totalCart = 0;
        for (let item in cart) {
            if (cart.hasOwnProperty(item)) {
                totalCart += cart[item].Price * cart[item].Quantity;
            }
        }
        return Number(totalCart.toFixed(2));
    }

    // List cart
    obj.listCart = function () {
        const cartCopy = [];
        for (let i in cart) {
            const item = cart[i];
            const itemCopy = {};
            for (let p in item) {
                itemCopy[p] = item[p];
            }

            itemCopy.total = Number(item.Price * item.Quantity).toFixed(2);
            cartCopy.push(itemCopy)
        }
        return cartCopy;
    }

    // get added product
    obj.getProduct = function (id) {
        for (let i in cart) {
            if (cart[i].ProductQuantitySetId === id) {
               return cart[i];
            }
        }
        return null;
    };


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
//$('.add-to-cart').click(function (event) {
//    event.preventDefault();

//    const name = $(this).data('name');
//    const price = Number($(this).data('price'));

//    shoppingCart.addItemToCart(name, price, 1);
//    displayCart();
//});

// Clear items
//$('.clear-cart').click(function () {
//    shoppingCart.clearCart();
//    displayCart();
//});


function displayCart() {
    const cartArray = shoppingCart.listCart();
    var output = "";
    for (let i in cartArray) {
        output += `<tr>
                    <td class="text-left">${cartArray[i].Name}</td>
                    <td>৳${cartArray[i].Price}</td>
                    <td class="text-center">
                     <input class="item-quantity" data-id="${cartArray[i].ProductQuantitySetId}" value="${cartArray[i].Quantity}" min="0" type="number">
                    </td>
                    <td>৳${cartArray[i].total}</td>
                    <td class="text-right">
                        <button data-id="${cartArray[i].ProductQuantitySetId}" type="button" class="btn btn-sm btn-danger delete-item" data-toggle="tooltip" data-placement="top" title="Remove item">X</button>
                    </td>
                </tr>`
    }

    $('.show-cart tbody').html(output);
    $('.grand-total-amount').html(shoppingCart.totalCart());
    $('.total-cart-count').html(shoppingCart.totalCount());

    //<div class="btn-group radio-group ml-2" data-toggle="buttons">
    //    <label class="btn btn-sm btn-elegant btn-rounded minus-item" data-id="${cartArray[i].ProductQuantitySetId}">
    //        <input type="radio" name="radio-quantity">&mdash;
    //    </label>
    //    <label class="btn btn-sm btn-elegant btn-rounded plus-item" data-id="${cartArray[i].ProductQuantitySetId}">
    //        <input type="radio" name="radio-quantity">+
    //    </label>
    //</div>
}

// Delete item button
$('.show-cart').on("click", ".delete-item", function (event) {
    const id = $(this).data('id');

    shoppingCart.removeProduct(id);
    displayCart();
})


// -1
$('.show-cart').on("click", ".minus-item", function (event) {
    const id = $(this).data('id');
    shoppingCart.decreaseQuantity(id);

    displayCart();
})


// +1
$('.show-cart').on("click", ".plus-item", function (event) {
    const id = $(this).data('id');

    shoppingCart.increaseQuantity(id);
    displayCart();
})


// Item quantity input
$('.show-cart').on("change", ".item-quantity", function (event) {
    const id = $(this).data('id');

    if (isNaN($(this).val())) return;

    const quantity = Number($(this).val());

    shoppingCart.inputQuantity(id, quantity);
    displayCart();
});


displayCart();
