
const shoppingCart = (function () {
   let cart = [];

    // Save cart
    function saveCart() {
        localStorage.setItem('shoppingCart', JSON.stringify(cart));
    }

    // Load cart
    function loadCart() {
        cart = JSON.parse(localStorage.getItem('shoppingCart'));
    }

    if (localStorage.getItem("shoppingCart") != null) {
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

    return obj;
})();


// *****************************************
// Triggers / Events
// ***************************************** 

function displayCart() {
    const cartArray = shoppingCart.listCart();
    var output = "";
    for (let i in cartArray) {
        output += `<tr>
                    <td class="text-left">
                     <p class="mb-0">${cartArray[i].Name}</p>
                      <h5 class="mb-0">${addAttribute(cartArray[i].attributesValue)}</h5>
                    </td>
                    <td>৳${cartArray[i].Price}</td>
                    <td class="text-center">
                     <input class="item-quantity" data-id="${cartArray[i].ProductQuantitySetId}" value="${cartArray[i].Quantity}" min="1" type="number">
                    </td>
                    <td>৳${cartArray[i].total}</td>
                    <td class="text-right">
                        <button data-id="${cartArray[i].ProductQuantitySetId}" type="button" class="btn btn-sm grey darken-3 text-white delete-item" data-toggle="tooltip" data-placement="top" title="Remove item">X</button>
                    </td>
                </tr>`
    }

    const emptyRow = `<tr><td colspan="5" class="alert alert-danger">No Product Added</td></tr>`;
    $('.show-cart tbody').html(shoppingCart.totalCount() ? output : emptyRow);
    $('.grand-total-amount').html(shoppingCart.totalCart());
    $('.total-cart-count').html(shoppingCart.totalCount());

    shoppingCart.totalCount() ? $('.modal-footer').show() : $('.modal-footer').hide();
}

function addAttribute(attributes) {
    let attr = "";
    attributes.forEach(att => {
        attr += `<span class="badge badge-pill grey darken-3 mr-2">${att.KeyName}: ${att.Value}</span>`;
    })
    return attr;
}

// Item quantity input
$('.show-cart').on("change", ".item-quantity", function (event) {
    const self = $(this);
    const id = self.data('id');
    const quantity = Number($(this).val());

    $.ajax({
        url: "/Product/GetAvailableQuantity",
        data: { quantitySetId: id },
        success: function (response) {
            if (response.IsSuccess) {
                self.attr("max", response.Data);
                if (response.Data < quantity) return;

                shoppingCart.inputQuantity(id, quantity);
                displayCart();
            }
        },
        error: function (err) {
            console.log(err);
        }
    })
});


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


displayCart();
