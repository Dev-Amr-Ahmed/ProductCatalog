$(document).on('click', '#deleteProductButton', function () {
    var productId = $(this).data('id');
    $('.modal-body #productId').val(productId);
});

$('#DeleteButton').on('click', function () {
    var productId = $('.modal-body #productId').val();
    $.ajax({
        type: 'POST',
        'url': '/Product/DeleteProduct',
        data: { productId: productId },
        success: function (data) {
            window.location.reload();
        }
    });
});