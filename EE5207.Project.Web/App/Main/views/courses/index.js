(function () {
    angular.module('app').controller('app.views.courses.index', [
        '$scope', '$timeout', '$uibModal', 'abp.services.app.course', 'appSession',
        function ($scope, $timeout, $uibModal, courseService, appSession) {
            var vm = this;

            vm.courses = [];

            vm.attendance = {};

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

            vm.markAttendance = function (course) {
                abp.message.confirm(
                    "Start course '" + course.name + "'?",
                    function (result) {
                        if (result) {
                            courseService.markAttendance(course)
                                .then(function () {
                                    abp.notify.info("Started course: " + course.name);
                                    getCourses();
                                });
                        }
                    });
            };

            vm.markStudent = function () {
                courseService.markStudent(3, "9D4DAC5C-3974-4C75-B3DB-87E1319EB06B")
                    .then(function (resu) {
                        courseService.getAttendance(resu.data)
                            .then(function (res) {
                                vm.attendance = res.data;
                                console.log(vm.attendance);
                                vm.attendance.presentDays++;
                                console.log(vm.attendance);
                                courseService.updateAttendance(vm.attendance)
                                    .then(function (r) {
                                        console.log(r);
                                        abp.notify.success("Done!");
                                    });
                            });
                    });

            };



            vm.enrollStudent = function (course) {
                courseService.enrollStudent(appSession.user.id, course.id);
                abp.notify.success("You are enrolled in " + course.name + "!");
            };


            vm.refresh = function () {
                getCourses();
            };

            getCourses();
        }
    ]);
})();