////const { data } = require("jquery");
////

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
            CART.increase(id, 1);
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
        url: '/Product/Cart',
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



    
    

