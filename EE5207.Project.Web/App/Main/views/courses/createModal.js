(function () {
    angular.module('app').controller('app.views.courses.createModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.course',
        function ($scope, $uibModalInstance, courseService) {
            var vm = this;

            vm.course = {};

            vm.save = function () {
                courseService.create(vm.course)
                    .then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };


        }
    ]);
})();