const Products = $('.product');
let CategoryFilter = [];

const HandleCheckbox = (id) => {
    let target = $(id).children('.checkbox').first();
    let category = target.category;

    if (!target.hasClass('active')) {
        target.addClass('active');
        CategoryFilter.push(category);
    } else {
        target.removeClass('active');
        CategoryFilter.filter((c) => c != category);
    }

    Products.filter((product) => CategoryFilter.includes(product.category));
}