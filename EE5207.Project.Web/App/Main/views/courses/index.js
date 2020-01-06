(function () {
    angular.module('app').controller('app.views.courses.index', [
        '$scope', '$timeout', '$uibModal', 'abp.services.app.course',
        function ($scope, $timeout, $uibModal, courseService) {
            var vm = this;

            vm.courses = [];

            function getCourses() {
                courseService.getAll({}).then(function (result) {
                    vm.courses = result.data.items;
                });
            }

            vm.openCourseCreationModal = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/courses/createModal.cshtml',
                    controller: 'app.views.courses.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });

                modalInstance.result.then(function () {
                    getCourses();
                });
            };

            vm.openCourseDetailModal = function (course) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/courses/detailModal.cshtml',
                    controller: 'app.views.courses.detailModal as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return course.id;
                        }
                    }
                });

                modalInstance.rendered.then(function () {
                    $timeout(function () {
                        $.AdminBSB.input.activate();
                    }, 0);
                });

                modalInstance.result.then(function () {
                    getCourses();
                });
            };

            vm.delete = function (course) {
                abp.message.confirm(
                    "Delete course '" + course.name + "'?",
                    function (result) {
                        if (result) {
                            courseService.delete({ id: course.id })
                                .then(function () {
                                    abp.notify.info("Deleted course: " + course.name);
                                    getCourses();
                                });
                        }
                    });
            };


            vm.refresh = function () {
                getCourses();
            };

            getCourses();
        }
    ]);
})();