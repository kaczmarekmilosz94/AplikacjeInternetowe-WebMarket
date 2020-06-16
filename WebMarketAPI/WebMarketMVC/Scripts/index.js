const HandleCheckbox = (id) => {
    let target = $(id).children('.checkbox').first();

    if (!target.hasClass('active')) {
        target.addClass('active');
    } else {
        target.removeClass('active');
    }
}