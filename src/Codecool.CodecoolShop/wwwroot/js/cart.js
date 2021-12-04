let PRODUCTS = [];
const CART = {
    KEY: 'bkasjbdfkjasdkfjhaksdfjskd',
    contents: [],
    init() {

        let _contents = localStorage.getItem(CART.KEY);
        if (_contents) {
            CART.contents = JSON.parse(_contents);
        } else {
            CART.contents = [];
            CART.sync();
        }
    },
    async sync() {
        let _cart = JSON.stringify(CART.contents);
        await localStorage.setItem(CART.KEY, _cart);
    },
    find(id) {
        let match = CART.contents.filter(item => {
            if (item.id == id) {
                return true;
            }
        });
        if (match && match[0])
            return match[0];
    },
    add(id, item = null) {
        if (CART.find(id)) {
            if (item != null) {
                CART.increase(id, item.quanity);
            }
            else {
                CART.increase(id, 1);
            }

        } else {
            PRODUCTS.push(item);
            CART.contents.push(item);
            CART.sync();
        }

    },
    increase(id, qty = 1) {
        CART.contents = CART.contents.map(item => {
            if (item.id == id)
                item.quanity = item.quanity + qty;
            return item;
        });
        CART.sync()
    },
    reduce(id, qty = 1) {
        CART.contents = CART.contents.map(item => {
            if (item.id === id)
                item.quanity = item.quanity - qty;
            return item;
        });
        CART.contents.forEach(async item => {
            if (item.id === id && item.quanity === 0)
                await CART.remove(id);
        });
        CART.sync
    },
    remove(id) {
        CART.contents = CART.contents.filter(item => {
            if (item.id !== id) {
                return true;
            }
        });
        CART.sync();
    },
    empty() {
        CART.contents = [];
        CART.sync();
    }

};

document.addEventListener('DOMContentLoaded', () => {
    CART.init();
    console.log(CART.contents);
});

const goToCartButton = document.getElementById('go-to-cart');
goToCartButton.addEventListener('click', () => {
    var content = JSON.stringify(CART.contents);
    $.ajax({
        type: "POST",
        traditional: true,
        url: '/Cart/Cart',
        data: { content: content },
        dataType: "json",
        success: function (data) {
            if (data) {
                window.location.href = data;
            }
        }
    });
})

document.querySelectorAll("#add-to-cart").forEach(item => {
    item.addEventListener("click", (ev) => {
        getProduct(ev);
    })
})

function incrementCart(ev) {
    ev.preventDefault();
    let id = parseInt(ev.target.getAttribute('data-id'));
    CART.increase(id, 1);
    let controls = ev.target.parentElement;
    let qty = controls.querySelector('span');
    if (item) {
        qty.textContent = item.quantity;
    } else {
        document.getElementById('cart').removeChild(controls.parentElement);
    }
}

function decrementCart(ev) {
    ev.preventDefault();
    let id = parseInt(ev.target.getAttribute('data-id'));
    CART.reduce(id, 1);
    let controls = ev.target.parentElement;
    let qty = controls.innerText;
    let item = CART.find(id);
    if (item) {
        qty.textContent = item.quantity;
    } else {
        document.getElementById('cart').removeChild(controls.parentElement.parentElement);
    }
}

function removeFromCart(ev) {
    ev.preventDefault();
    let id = parseInt(ev.target.getAttribute('data-id'));
    CART.remove(id);
    let controls = ev.target.parentElement;
    document.getElementById('cart').removeChild(controls.parentElement);
}

function getUserProducts() {
    let productsInCart = querySelectorAll(".cart-item");
    productsInCart.forEach(x => {
        let id = parseInt(x.getAttribute('data-id'));
        let name = x.childNodes[0].childNodes[1].childNodes[0].getAttribute('id');
        let description = x.getAttribute('data-description');
        let price = new Intl.NumberFormat('en-CA',
            { style: 'currency', currency: 'CAD' }).format(x.childNodes[1].getAttribute['data-price']);
        let supplier = x.getAttribute('data-supplier');
        let category = x.getAttribute('data-category');
        let item = {
            id: id,
            name: name,
            description: description,
            price: price,
            supplier: supplier,
            category: category,
            quanity: x.getAttribute('data-quantity')
        }
        CART.add(id, item);
    })
}


function getProduct(ev) {
    ev.preventDefault();
    let id = parseInt(ev.target.getAttribute('data-id'));
    let product = ev.target.parentElement;
    let name = product.childNodes[3].innerHTML;
    let description = product.childNodes[5].innerHTML;
    let price = new Intl.NumberFormat('en-CA',
        { style: 'currency', currency: 'CAD' }).format(product.childNodes[11].innerText.substring(8));
    let supplier = product.childNodes[9].innerHTML.substring(10);
    let category = product.childNodes[7].innerHTML.substring(10);
    let item = {
        id: id,
        name: name,
        description: description,
        price: price,
        supplier: supplier,
        category: category,
        quanity: 1
    }
    console.log(item);
    CART.add(id, item);
}

function addItem(ev) {
    ev.preventDefault();
    let id = parseInt(ev.target.getAttribute('data-id'));
    CART.add(id);
}

document.querySelectorAll('#minus').forEach(button => {
    button.addEventListener("click", (ev) => {
        decrementCart(ev);
        if (CART.find(parseInt(ev.target.getAttribute('data-id')))) {
            ev.target.parentElement.querySelector('span').innerText = CART.find(parseInt(ev.target.getAttribute('data-id'))).quanity;
        }

    })
})

document.querySelectorAll('#plus').forEach(button => {
    button.addEventListener('click', (ev) => {
        addItem(ev);
        ev.target.parentElement.querySelector('span').innerText = CART.find(parseInt(ev.target.getAttribute('data-id'))).quanity;
    })
})

document.querySelectorAll('#remove').forEach(button => {
    button.addEventListener('click', (ev) => {
        removeFromCart(ev);
    })
})
