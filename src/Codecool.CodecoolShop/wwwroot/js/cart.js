const CART = {
    KEY: 'bkasjbdfkjasdkfjhaksdfjskd',
    contents: [],
    init() {
        let _contents = localStorage.getItem(CART.KEY);
        if (_contents) {
            CART.contents = JSON.parse(_contents);
        } else {
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
    add(id) {
        if (CART.find(id)) {
            CART.increase(id, 1);
        } else {
            let arr = PRODUCTS.filter(product => {
                if (product.id == id) {
                    return true;
                }
            });
            if (arr && arr[0]) {
                let obj = {
                    id: arr[0].id,
                    name: arr[0].name,
                    price: arr[0].price,
                    description: arr[0].description,
                    suplier: arr[0].suplier,
                    quanity: 1,
                    category: arr[0].category
                };
                CART.contents.push(obj);
                CART.sync();
            }
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
let PRODUCTS = [];

document.addEventListener('DOMContentLoaded', () => {
    getProducts(showProducts, errorMessage);
    CART.init();
    showCart();
});

function showCart() {
    let cartSection = document.getElementById('cart');
    let s = CART;
    s.forEach(item => {
        let cartitem = document.createElement('tr');
        cartitem.className = 'cart-item';
        let imgtd = document.createElement('td');
        imgtd.className = 'image border-0';
        let imgdiv = document.createElement('div');
        imgdiv.className = 'p-2';
        let img = document.createElement('img');
        img.src = URL.createObjectURL(`~/img${item.name}.jpg`);
        imgdiv.appendChild(img);
        imgtd.appendChild(imgdiv);
        cartitem.appendChild(imgtd);
        let price = document.createElement('td');
        price.className = 'price border-0 align-middle';
        price.textContent = item.price;
        cartitem.appendChild(price);
        let minus = document.createElement('button');
        minus.className = 'minus btn btn-danger';
        let quantity = document.createElement('td');
        quantity.className = 'border-0 align-middle';
        quantity.appendChild(minus);
        quantity.textContent = `${item.quantity}`;
        let plus = document.createElement('button');
        plus.className = 'plus btn btn-success';
        quantity.appendChild(plus);
        cartitem.appendChild(quantity);
        let remove = document.createElement('td');
        remove.className = 'border-0 align-middle';
        let removeButton = document.createElement('button');
        removeButton.className = 'remove btn btn-outline-secondary';
        removeButton.innerHTML = "x";
        remove.appendChild(removeButton);
        cartitem.appendChild(remove);
    })
}