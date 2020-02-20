(function () {
    angular.module('app').controller('app.views.courses.attendanceModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.course', 'id',
        function ($scope, $uibModalInstance, courseService, id) {
            var vm = this;

            vm.studentId;

            var init = function () {
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

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

            vm.mark = function () {
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

            init();
        }
    ]);
})();