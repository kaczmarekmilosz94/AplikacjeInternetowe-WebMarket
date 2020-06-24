const HTTP_SERVER = "https://localhost:44373/"
const Products = $('.product');
let CategoryFilter = [];
let currentPage = 0;

const HandleCheckbox = (id) => {
    const root = $(id);
    const target = root.children('.checkbox').first();
    const category = String(root.attr('id'));

    // Color checkbox and set active filters
    if (!target.hasClass('active')) {
        target.addClass('active');
        CategoryFilter.push(category);
    } else {
        target.removeClass('active');
        CategoryFilter = CategoryFilter.filter(c => c != category);
    }

    // Filter products
    Products.each((index, p) => {
        let product = $(p);

        if (CategoryFilter.length <= 0) {
            product.fadeIn(500);
            product.removeClass('filtered');
        } else if (CategoryFilter.includes(product.attr('category'))) {
            product.fadeIn(500);
            product.removeClass('filtered');
        } else {
            product.fadeOut(500);
            product.addClass('filtered');
        }
    });

    Paginate(currentPage);
}

const GetUnfilteredProducts = () => { return Products.filter((index, product) => !$(product).hasClass('filtered')) }

const Paginate = (page, pageSize = 10) => {
    const filteredProducts = GetUnfilteredProducts();
    const maxPage = parseInt((filteredProducts.length-1) / pageSize);
    if (maxPage < page) { page = maxPage; }
    currentPage = page;

    filteredProducts.each((index, product) => {
        if (index >= page * pageSize && index < page * pageSize + pageSize) {
            $(product).fadeIn(500);
        } else {
            $(product).hide();
        }
    });

    $('#pages').html(`${currentPage+1} / ${maxPage+1}`);
};

const PageNext = (pageSize = 10) => {
    if (GetUnfilteredProducts().length > (currentPage+1) * pageSize) {
        Paginate(currentPage + 1, pageSize);
    }
}

const PagePrev = (pageSize = 10) => {
    if (currentPage > 0) {
        Paginate(currentPage - 1, pageSize);
    }
}

const AddToBasket = (userId = 0, productId = 0) => {
    fetch(`${HTTP_SERVER}/api/Users/${userId}/basket/add `, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(productId)
    }).then(
        response => console.log(response)
    );
};

$(document).ready(() => {
    Paginate(currentPage);
});