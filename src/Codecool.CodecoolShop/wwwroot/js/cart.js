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


function showCart() {
    console.log("cart");
    let cartSection = document.getElementById('cart');
    let s = CART.contents;
    s.forEach(item => {
        let cartitem = document.createElement('tr');
        cartitem.className = 'cart-item';
        let imgtd = document.createElement('td');
        imgtd.className = 'image border-0';
        let imgdiv = document.createElement('div');
        imgdiv.className = 'p-2';
        let img = document.createElement('img');
        img.src = `~/img${item.name}.jpg`;
        imgdiv.appendChild(img);
        imgtd.appendChild(imgdiv);
        cartitem.appendChild(imgtd);
        let price = document.createElement('td');
        let cost = new Intl.NumberFormat('en-CA',
            { style: 'currency', currency: 'CAD' }).format(item.price);
        price.textContent = cost;
        price.className = 'price border-0 align-middle';
        cartitem.appendChild(price);

        let controls = document.createElement('td');
        controls.className = 'controls';
        cartitem.appendChild(controls);
        let minus = document.createElement('button');
        minus.className = 'minus btn btn-danger';
        minus.setAttribute('data-id', item.id);
        controls.appendChild(minus);
        let quantity = document.createElement('span');
        quantity.className = 'border-0 align-middle';
        quantity.textContent = `${item.quantity}`;
        quantity.setAttribute('data-id', item.id);
        controls.appendChild(quantity);
        let plus = document.createElement('button');
        plus.className = 'plus btn btn-success';
        plus.setAttribute('data-id', item.id);
        controls.appendChild(plus);

        cartitem.appendChild(controls);
        let priceTotal = document.createElement('td');
        let costTotal = new Intl.NumberFormat('en-CA',
            { style: 'currency', currency: 'CAD' }).format(item.quantity * item.price);
        priceTotal.textContent = costTotal;
        priceTotal.className = 'price border-0 align-middle';
        cartitem.appendChild(priceTotal);

        let remove = document.createElement('td');
        remove.className = 'border-0 align-middle';
        let removeButton = document.createElement('button');
        removeButton.className = 'remove btn btn-outline-secondary';
        removeButton.innerHTML = "x";
        remove.setAttribute('data-id', item.id);
        remove.appendChild(removeButton);
        cartitem.appendChild(remove);
        cartSection.appendChild(cartitem);
        console.log(cartitem);
        console.log(item);
    })
}

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
    let qty = controls.querySelector('span');
    if (item) {
        qty.textContent = item.quantity;
    } else {
        document.getElementById('cart').removeChild(controls.parentElement);
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
    console.log(item);
    CART.add(id, item);
    console.log(CART);
}

function addItem(ev) {
    console.log("nsdjd");
    ev.preventDefault();
    let id = parseInt(ev.target.getAttribute('data-id'));
    CART.add(id, 1);
    showCart();
}