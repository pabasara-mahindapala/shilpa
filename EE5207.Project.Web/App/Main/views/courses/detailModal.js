(function () {
    angular.module('app').controller('app.views.courses.detailModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.course', 'id',
        function ($scope, $uibModalInstance, courseService, id) {
            var vm = this;

            vm.course = {};
            vm.students = [];

            var init = function () {
                courseService.get({ id: id })
                    .then(function (result) {
                        vm.course = result.data;
                        courseService.getStudents(id)
                            .then(function (res) {
                                vm.students = res.data;
                            });
                    });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

            init();
        }
    ]);
})();