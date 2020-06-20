const HTTP_SERVER = "https://localhost:44349"
const Products = $('.product');
let CategoryFilter = [];

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
        } else if (CategoryFilter.includes(product.attr('category'))) {
            product.fadeIn(500);
        } else {
            product.fadeOut(500);
        }
    });
}

const AddToBasket = (id) => {
    fetch(`${HTTP_SERVER}/`, () => {

    });
}