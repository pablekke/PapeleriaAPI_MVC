function showToast(mensajeExito, mensajeEliminacionExitosa) {
    let toastHtml = `
        <div class="toast mx-auto" role="alert" aria-live="assertive" aria-atomic="true" data-delay="2000">
            <div class="toast-header text-black" style="background-color: #a9cf93">
                <strong class="mr-auto">Notificación</strong>
                <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="toast-body bg-dark text-light">
                ${mensajeExito || mensajeEliminacionExitosa}
            </div>
        </div>
    `;

    // Append the toast to the body
    $('body').append(toastHtml);

    // Show the toast
    $('.toast').toast('show');

    // Remove the toast from the DOM after it hides
    $('.toast').on('hidden.bs.toast', function () {
        $(this).remove();
    });
}
