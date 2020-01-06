(function () {
    angular.module('app').controller('app.views.users.detailModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.user', 'id',
        function ($scope, $uibModalInstance, userService, id) {
            var vm = this;

            vm.user = {};

            vm.courses = [];

            function getAttendance() {
                userService.getUserAttendances(id)
                    .then(function (result) {
                        for (var i = 0; i < result.data.length/2; i++) {
                            vm.courses.push({
                                name: result.data[i],
                                percentage: result.data[i + result.data.length / 2]
                            })
                        }
                    });
            }

            var init = function () {
                userService.get({ id: id })
                    .then(function (result) {
                        vm.user = result.data;
                    });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

            getAttendance();
            init();
        }
    ]);
})();