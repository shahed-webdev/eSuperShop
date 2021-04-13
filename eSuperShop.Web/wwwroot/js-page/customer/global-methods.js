
const methods = (function () {

    const methodObj = {};

    //get total cart count
    const setCount = document.querySelector(".total-cart-count");
    methodObj.getTotalCartCount = function () {
        $.ajax({
            url: "/Product/CartProductCount",
            success: function (response) {
                setCount.textContent = response;
            },
            error: function (err) {
                console.log(err)
            }
        });
    }

    //set from add to cart page
    methodObj.setCountAddToCart = function(count) {
        setCount.textContent = count;
    }

    //set quantity
    methodObj.setProductQuantity = function(element) {
        element.disabled = true;

        $.ajax({
            url: "/Product/PostQuantity",
            type: "POST",
            data: { orderCartId: element.id, quantity: element.value },
            success: function(response) {
                $.notify(response.Message, response.IsSuccess ? "success" : "error");

                setCount.textContent = response.Data;
                element.disabled = false;
            },
            error: function(err) {
                console.log(err);
                element.disabled = false;
            }
        });
    }

    //set selected product
    methodObj.allProductSelectedSet = function() {
        const checkboxes = document.getElementsByName("productCheckbox");
        const model = { OrderCartIds: [], IsSelected: true };

        for (let i = 0; i < checkboxes.length; i++) {
            //push id for checked product
            const orderCartId = checkboxes[i].getAttribute("data-id");
            model.OrderCartIds.push(orderCartId);
            model.IsSelected = checkboxes[i].checked;
        }

        if (!model.OrderCartIds.length) return;

        $.ajax({
            url: "/Product/SetSelectedProduct",
            type: "POST",
            data: model,
            success: function(response) {
                $.notify(response.Message, response.IsSuccess ? "success" : "error");
            },
            error: function(err) {
                console.log(err);
            }
        });
    }

    //set selected product
    methodObj.signleProductSelectedSet = function (self) {
        const model = { OrderCartIds: [], IsSelected: self.checked };
        model.OrderCartIds.push(self.getAttribute("data-id"));

        if (!model.OrderCartIds.length) return;

        $.ajax({
            url: "/Product/SetSelectedProduct",
            type: "POST",
            data: model,
            success: function (response) {
                $.notify(response.Message, response.IsSuccess ? "success" : "error");
            },
            error: function (err) {
                console.log(err);
            }
        });

    }

    return methodObj;
})();