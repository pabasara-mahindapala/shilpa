(function () {
    var controllerId = 'app.views.layout.topbar';
    angular.module('app').controller(controllerId, [
        '$rootScope', '$state', 'appSession', 'abp.services.app.user',
        function ($rootScope, $state, appSession, userService) {
            var vm = this;
            vm.userNotifications = [];

            function getUserNotifications() {
                userService.getUserNotifications(appSession.user.id).then(function (result) {
                    vm.userNotifications = result.data.items;
                });
            }

            abp.event.on('abp.notifications.received', function (userNotification) {
                getUserNotifications();
            });

            getUserNotifications();
        }
    ]);
})();