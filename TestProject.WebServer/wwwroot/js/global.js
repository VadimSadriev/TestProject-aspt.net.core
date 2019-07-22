const toast = (function () {
    'use strict';

    function toastTemplate(options) {

        return `<div class="toast" role="alert" aria-live="assertive" aria-atomic="true">
                    <div class="toast-header ">
                        <strong class="mr-auto ${options.colorText ? `text-${options.colorText}` : ''}">${options.headerText}</strong>
                        <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="toast-body">
                        ${options.bodyText}
                    </div>
                </div>`;
    }

    const defaultToastSettings = {
        autohide: true,
        delay: 5000
    };

    const defaultToastOptions = {
        headerText: 'Уведомление',
        bodyText: ''
    };

    return {
        alert: function (options) {

            options = {
                ...defaultToastOptions,
                ...options
            };

            const container = $('#toastContainer');

            const toastHtml = options
                ? toastTemplate(options)
                : toastTemplate();

            const toast = $(toastHtml);

            container.append(toast);

            toast.toast({
                ...defaultToastSettings,
                ...options
            });
            toast.toast('show');
        },
        succes: function (options) {
            options = {
                headerText: 'Уведомление',
                colorText: 'success',
                ...options
            };
            this.alert(options);
        },
        error: function (options) {
            options = {
                headerText: 'Ошибка',
                colorText: 'danger',
                ...options
            };
            this.alert(options);
        }
    };
})();

(function () {
    try {
        const hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(`${location.origin}/requestnotification`)
            .build();

        hubConnection.on("NotifyAdmins", function (message) {
            toast.succes({ bodyText: message });
        });

        hubConnection.start();
    }
    catch (err) {
        console.log(err);
    }

})();