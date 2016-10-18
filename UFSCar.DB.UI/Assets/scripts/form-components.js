var FormComponents = function () {

    var handleWysihtml5 = function () {
        if (!jQuery().wysihtml5) {
            return;
        }

        if ($('.wysihtml5').size() > 0) {
            $('.wysihtml5').wysihtml5({
                "stylesheets": ["assets/plugins/bootstrap-wysihtml5/wysiwyg-color.css"]
            });
        }
    }

    var handleToggleButtons = function () {
        if (!jQuery().toggleButtons) {
            return;
        }
        $('.basic-toggle-button').toggleButtons();
        $('.text-toggle-button').toggleButtons({
            width: 200,
            label: {
                enabled: "Lorem Ipsum",
                disabled: "Dolor Sit"
            }
        });
        $('.danger-toggle-button').toggleButtons({
            style: {
                // Accepted values ["primary", "danger", "info", "success", "warning"] or nothing
                enabled: "danger",
                disabled: "info"
            }
        });
        $('.info-toggle-button').toggleButtons({
            style: {
                enabled: "info",
                disabled: ""
            }
        });
        $('.success-toggle-button').toggleButtons({
            style: {
                enabled: "success",
                disabled: "info"
            }
        });
        $('.warning-toggle-button').toggleButtons({
            style: {
                enabled: "warning",
                disabled: "info"
            }
        });

        $('.height-toggle-button').toggleButtons({
            height: 100,
            font: {
                'line-height': '100px',
                'font-size': '20px',
                'font-style': 'italic'
            }
        });
    }

    var handleTagsInput = function () {
        if (!jQuery().tagsInput) {
            return;
        }
        $('#tags_1').tagsInput({
            width: 'auto',
            'onAddTag': function () {
                //alert(1);
            },
        });
        $('#tags_2').tagsInput({
            width: 240
        });
    }

    var handleDatePickers = function () {

        if (jQuery().datepicker) {
            $(".date-picker").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                language: 'pt-BR'
            });
        }
    }

    var handleTimePickers = function () {

        if ($('#hfRangeDataSelecionado')
            && $('#hfRangeDataSelecionado') != undefined && $('#hfRangeDataSelecionado') != 'undefined' && $('#hfRangeDataSelecionado').length > 0) {
            var dataInicio;

            var qtdDias = $('#hfRangeDataSelecionado').attr("qtdDias");
            if (qtdDias != null && qtdDias != undefined && qtdDias > 0)
                dataInicio = Date.today().add({ days: qtdDias * -1 });
            else
                dataInicio = Date.today().add({ days: -29 });

            var dataFim = Date.today();
            $('#hfRangeDataSelecionado').val(dataInicio.toString('dd/MM/yyyy') + "|" + dataFim.toString('dd/MM/yyyy'));
            var method = eval('(' + $('#hfRangeDataSelecionado').attr("funcaoCallback") + ')');
            method(dataInicio.toString('dd/MM/yyyy'), dataFim.toString('dd/MM/yyyy'));
        }

        if (jQuery().datepicker) {

            $(".date-picker").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                language: 'pt-BR'
            });


        }

        if (jQuery().timepicker) {
            $('.timepicker-default').timepicker();
            $('.timepicker-24').timepicker({
                minuteStep: 1,
                showSeconds: true,
                showMeridian: false
            });
        }
    }

    var handleDateRangePickers = function () {
        if (!jQuery().daterangepicker) {
            return;
        }

        $('.date-range').daterangepicker();

        $('.date-range-concurso').daterangepicker({
            endDate: Date.today().add({ 'days': 90 }),
            minDate: Date.today(),
            maxDate: Date.today().add({ 'days': 90 })
        });

        $('#dashboard-report-range').daterangepicker({
            ranges: {
                'Últimos 3 dias': [Date.today().add({
                    days: -3
                }), 'today'],
                'Últimos 7 dias': [Date.today().add({
                    days: -6
                }), 'today'],
                'Últimos 30 dias': [Date.today().add({
                    days: -29
                }), 'today'],
                'Este mês': [Date.today().moveToFirstDayOfMonth(), Date.today().moveToLastDayOfMonth()],
                'Último mês': [Date.today().moveToFirstDayOfMonth().add({
                    months: -1
                }), Date.today().moveToFirstDayOfMonth().add({
                    days: -1
                })],
                'Últimos 3 meses': [Date.today().moveToFirstDayOfMonth().add({
                    months: -3
                }), Date.today().moveToFirstDayOfMonth().add({
                    days: -1
                })]
            },
            opens: 'left',
            format: 'dd/MM/yyyy',
            separator: ' to ',
            startDate: Date.today().add({
                days: -29
            }),
            endDate: Date.today(),
            minDate: '01/01/2012',
            maxDate: '31/12/2014',
            showWeekNumbers: false,
            buttonClasses: ['btn-danger']
        },

        function (start, end) {
            App.blockUI(jQuery("#dashboard"));

            $('#dashboard-report-range span').html(start.toString('d MMMM, yyyy') + ' - ' + end.toString('d MMMM, yyyy'));

            if ($('#hfRangeDataSelecionado')
                 && $('#hfRangeDataSelecionado') != undefined && $('#hfRangeDataSelecionado') != 'undefined' && $('#hfRangeDataSelecionado').length > 0) {
                $('#hfRangeDataSelecionado').val(start.toString('dd/MM/yyyy') + "|" + end.toString('dd/MM/yyyy'));
                var method = eval('(' + $('#hfRangeDataSelecionado').attr("funcaoCallback") + ')');
                method(start.toString('dd/MM/yyyy'), end.toString('dd/MM/yyyy'));
            }
        });

        $('#dashboard-report-range').show();

        $('#dashboard-report-range span').html(Date.today().add({
            days: -29
        }).toString('d MMMM, yyyy') + ' - ' + Date.today().toString('d MMMM, yyyy'));

        $('#form-date-range').daterangepicker({
            ranges: {
                'Today': ['today', 'today'],
                'Yesterday': ['yesterday', 'yesterday'],
                'Last 7 Days': [Date.today().add({
                    days: -6
                }), 'today'],
                'Last 29 Days': [Date.today().add({
                    days: -29
                }), 'today'],
                'This Month': [Date.today().moveToFirstDayOfMonth(), Date.today().moveToLastDayOfMonth()],
                'Last Month': [Date.today().moveToFirstDayOfMonth().add({
                    months: -1
                }), Date.today().moveToFirstDayOfMonth().add({
                    days: -1
                })]
            },
            opens: (App.isRTL() ? 'left' : 'right'),
            format: 'MM/dd/yyyy',
            separator: ' to ',
            startDate: Date.today().add({
                days: -29
            }),
            endDate: Date.today(),
            minDate: '01/01/2012',
            maxDate: '12/31/2014',
            locale: {
                applyLabel: 'Aplicar',
                fromLabel: 'De',
                toLabel: 'Ate',
                customRangeLabel: 'Custom Range',
                daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                firstDay: 1
            },
            showWeekNumbers: true,
            buttonClasses: ['btn-danger']
        },

        function (start, end) {
            $('#form-date-range span').html(start.toString('MMMM d, yyyy') + ' - ' + end.toString('MMMM d, yyyy'));
        });

        $('#form-date-range span').html(Date.today().add({
            days: -29
        }).toString('MMMM d, yyyy') + ' - ' + Date.today().toString('MMMM d, yyyy'));


        //modal version:

        $('#form-date-range-modal').daterangepicker({
            ranges: {
                'Today': ['today', 'today'],
                'Yesterday': ['yesterday', 'yesterday'],
                'Last 7 Days': [Date.today().add({
                    days: -6
                }), 'today'],
                'Last 29 Days': [Date.today().add({
                    days: -29
                }), 'today'],
                'This Month': [Date.today().moveToFirstDayOfMonth(), Date.today().moveToLastDayOfMonth()],
                'Last Month': [Date.today().moveToFirstDayOfMonth().add({
                    months: -1
                }), Date.today().moveToFirstDayOfMonth().add({
                    days: -1
                })]
            },
            opens: (App.isRTL() ? 'left' : 'right'),
            format: 'MM/dd/yyyy',
            separator: ' to ',
            startDate: Date.today().add({
                days: -29
            }),
            endDate: Date.today(),
            minDate: '01/01/2012',
            maxDate: '12/31/2014',
            locale: {
                applyLabel: 'Aplicar',
                fromLabel: 'De',
                toLabel: 'Ate',
                customRangeLabel: 'Custom Range',
                daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                firstDay: 1
            },
            showWeekNumbers: true,
            buttonClasses: ['btn-danger']
        },

        function (start, end) {
            $('#form-date-range-modal span').html(start.toString('MMMM d, yyyy') + ' - ' + end.toString('MMMM d, yyyy'));
        });

        $('#form-date-range-modal span').html(Date.today().add({
            days: -29
        }).toString('MMMM d, yyyy') + ' - ' + Date.today().toString('MMMM d, yyyy'));

    }

    var handleDatetimePicker = function () {

        $(".form_datetimeAgendamento").datetimepicker({
            isRTL: App.isRTL(),
            format: "dd/mm/yyyy - hh:ii",
            todayBtn: true,
            language: 'pt-BR',
            startDate: App.ConverteData((new Date().setMinutes(new Date().getMinutes() + 10)), "dd/MM/yyyy HH:ss"),
            minuteStep: 5,
            pickerPosition: (App.isRTL() ? "bottom-right" : "bottom-left")
        });

        $(".form_datetimeMarco").datetimepicker({
            isRTL: App.isRTL(),
            format: "dd/mm/yyyy - hh:ii",
            todayBtn: true,
            language: 'pt-BR',
            startDate: Date.today().add({
                years: -1
            }),
            endDate: Date.today(),
            minuteStep: 5,
            pickerPosition: (App.isRTL() ? "bottom-right" : "bottom-left")
        });


        $('.form_datetimeAgendamento').on('changeDate', function (e) {
            console.log(e.date);
            $('#datahoraselecionada').html(App.ConverteData(e.date, "dd/mm/yyyy HH:ss"));
        });

        $(".form_datetime").datetimepicker({
            isRTL: App.isRTL(),
            format: "dd MM yyyy - hh:ii",
            todayBtn: true,
            language: 'pt-BR',
            minuteStep: 5,
            pickerPosition: (App.isRTL() ? "bottom-right" : "bottom-left")
        });

        $(".form_advance_datetime").datetimepicker({
            isRTL: App.isRTL(),
            format: "dd MM yyyy - hh:ii",
            autoclose: true,
            todayBtn: true,
            language: 'pt-BR',
            startDate: "2014-02-14 10:00",
            pickerPosition: (App.isRTL() ? "bottom-right" : "bottom-left"),
            minuteStep: 10
        });

        $(".form_meridian_datetime").datetimepicker({
            isRTL: App.isRTL(),
            format: "dd MM yyyy - HH:ii P",
            showMeridian: true,
            autoclose: true,
            pickerPosition: (App.isRTL() ? "bottom-right" : "bottom-left"),
            todayBtn: true,
            language: 'pt-BR'
        });
    }

    var handleClockfaceTimePickers = function () {

        if (!jQuery().clockface) {
            return;
        }

        $('.clockface_1').clockface();

        $('#clockface_2').clockface({
            format: 'HH:mm',
            trigger: 'manual'
        });

        $('#clockface_2_toggle').click(function (e) {
            e.stopPropagation();
            $('#clockface_2').clockface('toggle');
        });

        $('#clockface_2_modal').clockface({
            format: 'HH:mm',
            trigger: 'manual'
        });

        $('#clockface_2_modal_toggle').click(function (e) {
            e.stopPropagation();
            $('#clockface_2_modal').clockface('toggle');
        });

        $('.clockface_3').clockface({
            format: 'H:mm'
        }).clockface('show', '14:30');
    }

    var handleColorPicker = function () {
        if (!jQuery().colorpicker) {
            return;
        }
        $('.colorpicker-default').colorpicker({
            format: 'hex'
        });
        $('.colorpicker-rgba').colorpicker();
    }

    var handleSelect2 = function () {

        $('#select2_sample1').select2({
            placeholder: "Selecione uma Opção",
            allowClear: true
        });

        $('#select2_sample2').select2({
            placeholder: "Select a State",
            allowClear: true
        });

        $("#select2_sample3").select2({
            allowClear: true,
            minimumInputLength: 1,
            query: function (query) {
                var data = {
                    results: []
                }, i, j, s;
                for (i = 1; i < 5; i++) {
                    s = "";
                    for (j = 0; j < i; j++) {
                        s = s + query.term;
                    }
                    data.results.push({
                        id: query.term + i,
                        text: s
                    });
                }
                query.callback(data);
            }
        });

        function format(state) {
            if (!state.id) return state.text; // optgroup
            return "<img class='flag' src='assets/img/flags/" + state.id.toLowerCase() + ".png'/>&nbsp;&nbsp;" + state.text;
        }
        $("#select2_sample4").select2({
            allowClear: true,
            formatResult: format,
            formatSelection: format,
            escapeMarkup: function (m) {
                return m;
            }
        });

        $("#select2_sample5").select2({
            tags: ["red", "green", "blue", "yellow", "pink"]
        });


        function movieFormatResult(movie) {
            var markup = "<table class='movie-result'><tr>";
            if (movie.posters !== undefined && movie.posters.thumbnail !== undefined) {
                markup += "<td valign='top'><img src='" + movie.posters.thumbnail + "'/></td>";
            }
            markup += "<td valign='top'><h5>" + movie.title + "</h5>";
            if (movie.critics_consensus !== undefined) {
                markup += "<div class='movie-synopsis'>" + movie.critics_consensus + "</div>";
            } else if (movie.synopsis !== undefined) {
                markup += "<div class='movie-synopsis'>" + movie.synopsis + "</div>";
            }
            markup += "</td></tr></table>"
            return markup;
        }

        function movieFormatSelection(movie) {
            return movie.title;
        }

        $("#select2_sample6").select2({
            placeholder: "Search for a movie",
            minimumInputLength: 1,
            ajax: { // instead of writing the function to execute the request we use Select2's convenient helper
                url: "http://api.rottentomatoes.com/api/public/v1.0/movies.json",
                dataType: 'jsonp',
                data: function (term, page) {
                    return {
                        q: term, // search term
                        page_limit: 10,
                        apikey: "ju6z9mjyajq2djue3gbvv26t" // please do not use so this example keeps working
                    };
                },
                results: function (data, page) { // parse the results into the format expected by Select2.
                    // since we are using custom formatting functions we do not need to alter remote JSON data
                    return {
                        results: data.movies
                    };
                }
            },
            initSelection: function (element, callback) {
                // the input tag has a value attribute preloaded that points to a preselected movie's id
                // this function resolves that id attribute to an object that select2 can render
                // using its formatResult renderer - that way the movie name is shown preselected
                var id = $(element).val();
                if (id !== "") {
                    $.ajax("http://api.rottentomatoes.com/api/public/v1.0/movies/" + id + ".json", {
                        data: {
                            apikey: "ju6z9mjyajq2djue3gbvv26t"
                        },
                        dataType: "jsonp"
                    }).done(function (data) {
                        callback(data);
                    });
                }
            },
            formatResult: movieFormatResult, // omitted for brevity, see the source of this page
            formatSelection: movieFormatSelection, // omitted for brevity, see the source of this page
            dropdownCssClass: "bigdrop", // apply css that makes the dropdown taller
            escapeMarkup: function (m) {
                return m;
            } // we do not want to escape markup since we are displaying html in results
        });
    }

    var handleSelect2Modal = function () {

        $('#select2_sample_modal_1').select2({
            placeholder: "Selecione uma Opção",
            allowClear: true
        });

        $('#select2_sample_modal_2').select2({
            placeholder: "Select a State",
            allowClear: true
        });

        $("#select2_sample_modal_3").select2({
            allowClear: true,
            minimumInputLength: 1,
            query: function (query) {
                var data = {
                    results: []
                }, i, j, s;
                for (i = 1; i < 5; i++) {
                    s = "";
                    for (j = 0; j < i; j++) {
                        s = s + query.term;
                    }
                    data.results.push({
                        id: query.term + i,
                        text: s
                    });
                }
                query.callback(data);
            }
        });

        function format(state) {
            if (!state.id) return state.text; // optgroup
            return "<img class='flag' src='assets/img/flags/" + state.id.toLowerCase() + ".png'/>&nbsp;&nbsp;" + state.text;
        }
        $("#select2_sample_modal_4").select2({
            allowClear: true,
            formatResult: format,
            formatSelection: format,
            escapeMarkup: function (m) {
                return m;
            }
        });

        $("#select2_sample_modal_5").select2({
            tags: ["red", "green", "blue", "yellow", "pink"]
        });


        function movieFormatResult(movie) {
            var markup = "<table class='movie-result'><tr>";
            if (movie.posters !== undefined && movie.posters.thumbnail !== undefined) {
                markup += "<td valign='top'><img src='" + movie.posters.thumbnail + "'/></td>";
            }
            markup += "<td valign='top'><h5>" + movie.title + "</h5>";
            if (movie.critics_consensus !== undefined) {
                markup += "<div class='movie-synopsis'>" + movie.critics_consensus + "</div>";
            } else if (movie.synopsis !== undefined) {
                markup += "<div class='movie-synopsis'>" + movie.synopsis + "</div>";
            }
            markup += "</td></tr></table>"
            return markup;
        }

        function movieFormatSelection(movie) {
            return movie.title;
        }

        $("#select2_sample_modal_6").select2({
            placeholder: "Search for a movie",
            minimumInputLength: 1,
            ajax: { // instead of writing the function to execute the request we use Select2's convenient helper
                url: "http://api.rottentomatoes.com/api/public/v1.0/movies.json",
                dataType: 'jsonp',
                data: function (term, page) {
                    return {
                        q: term, // search term
                        page_limit: 10,
                        apikey: "ju6z9mjyajq2djue3gbvv26t" // please do not use so this example keeps working
                    };
                },
                results: function (data, page) { // parse the results into the format expected by Select2.
                    // since we are using custom formatting functions we do not need to alter remote JSON data
                    return {
                        results: data.movies
                    };
                }
            },
            initSelection: function (element, callback) {
                // the input tag has a value attribute preloaded that points to a preselected movie's id
                // this function resolves that id attribute to an object that select2 can render
                // using its formatResult renderer - that way the movie name is shown preselected
                var id = $(element).val();
                if (id !== "") {
                    $.ajax("http://api.rottentomatoes.com/api/public/v1.0/movies/" + id + ".json", {
                        data: {
                            apikey: "ju6z9mjyajq2djue3gbvv26t"
                        },
                        dataType: "jsonp"
                    }).done(function (data) {
                        callback(data);
                    });
                }
            },
            formatResult: movieFormatResult, // omitted for brevity, see the source of this page
            formatSelection: movieFormatSelection, // omitted for brevity, see the source of this page
            dropdownCssClass: "bigdrop", // apply css that makes the dropdown taller
            escapeMarkup: function (m) {
                return m;
            } // we do not want to escape markup since we are displaying html in results
        });
    }

    var handleMultiSelect = function () {
        $('#my_multi_select1').multiSelect();
        $('#my_multi_select2').multiSelect({
            selectableOptgroup: true
        });
    }

    var handleInputMasks = function () {
        $.extend($.inputmask.defaults, {
            'autounmask': true
        });

        $("#mask_date").inputmask("d/m/y", { autoUnmask: true });  //direct mask        
        $("#mask_date1").inputmask("d/m/y", { "placeholder": "*" }); //change the placeholder
        $("#mask_date2").inputmask("d/m/y", { "placeholder": "dd/mm/yyyy" }); //multi-char placeholder
        $("#mask_phone").inputmask("mask", { "mask": "(999) 999-9999" }); //specifying fn & options
        $("#mask_tin").inputmask({ "mask": "99-9999999" }); //specifying options only
        $("#mask_number").inputmask({ "mask": "9", "repeat": 10, "greedy": false });  // ~ mask "9" or mask "99" or ... mask "9999999999"
        $("#mask_decimal").inputmask('decimal', { rightAlignNumerics: false }); //disables the right alignment of the decimal input
        $("#mask_currency").inputmask('€ 999.999.999,99', { numericInput: true });  //123456  =>  € ___.__1.234,56

        $("#mask_currency2").inputmask('€ 999,999,999.99', { numericInput: true, rightAlignNumerics: false, greedy: false }); //123456  =>  € ___.__1.234,56
        $("#mask_ssn").inputmask("999-99-9999", { placeholder: " ", clearMaskOnLostFocus: true }); //default


        $(".maskdecimal").each(function () {
            $(this).maskMoney({ showSymbol: true, symbol: "%", decimal: ",", thousands: ".", allowZero: true, defaultZero: true });
        });


        $("*[mascara]").each(function () {
            $(this).inputmask($(this).attr("mascara"), { autoUnmask: true });  //direct mask 
        });

        $(".maskdecimal").each(function () {
            $(this).maskMoney({ showSymbol: false, symbol: "", decimal: ",", thousands: ".", allowZero: true, defaultZero: true, precision: $(this).attr("precisao") != undefined || $(this).attr("precisao") != null ? parseInt($(this).attr("precisao")) : 2 });
        });

        $(".masknegativo").each(function () {
            $(this).maskMoney({ showSymbol: false, symbol: "", decimal: ",", thousands: ".", allowZero: true, defaultZero: true, allowNegative: true });
        });

        $(".maskdolar").each(function () {
            $(this).maskMoney({ showSymbol: false, symbol: "", decimal: ".", thousands: ",", allowZero: true, defaultZero: true });
        });

        $(".maskdecimallimite").each(function () {
            $(this).maskMoney({ showSymbol: false, symbol: "", decimal: ",", thousands: ".", allowZero: true, defaultZero: true });

        }).on("blur",

            function () {

                var valor = null;
                var valorMaximo = null;

                if ($(this).val().length > 0)
                    valor = Number(Ihara.formataDecimal($(this).val()));

                if ($(this).attr("valormaximo") != undefined && $(this).attr("valormaximo").length > 0)
                    valorMaximo = Number(Ihara.formataDecimal($(this).attr("valormaximo")));

                if (valorMaximo != null)
                {
                    if (valor > valorMaximo)
                    {
                        var exibicaoValor = Ihara.formatMoney(valor, 2, ".", ",");
                        var exibicaoValorMaximo = Ihara.formatMoney(valorMaximo, 2, ".", ",");

                        $(this).val(exibicaoValorMaximo);
                        Ihara.showAlert('Valor informado(' + exibicaoValor + ') invalido. O valor foi alterado para o valor limite(' + exibicaoValorMaximo + ').');
                    }
                }


            }

        );

    }

    var handlePasswordStrengthChecker = function () {
        var initialized = false;
        var input = $("#password_strength");

        input.keydown(function () {
            if (initialized === false) {
                // set base options
                input.pwstrength({
                    raisePower: 1.4,
                    minChar: 8,
                    verdicts: ["Weak", "Normal", "Medium", "Strong", "Very Strong"],
                    scores: [17, 26, 40, 50, 60]
                });

                // add your own rule to calculate the password strength
                input.pwstrength("addRule", "demoRule", function (options, word, score) {
                    return word.match(/[a-z].[0-9]/) && score;
                }, 10, true);

                // set progress bar's width according to the input width
                $('.progress', input.parents('.password-strength')).css('width', input.outerWidth() - 2);

                // set as initialized 
                initialized = true;
            }
        });
    }

    var handleUsernameAvailabilityChecker1 = function () {
        var input = $("#username1_input");

        $("#username1_checker").click(function (e) {

            if (input.val() === "") {
                input.popover('destroy');
                input.popover({
                    'placement': App.isRTL() ? 'left' : 'right',
                    'html': true,
                    'title': 'Username Availability',
                    'content': 'Please enter a username to check its availability.',
                });
                // add error class to the popover
                input.data('popover').tip().addClass('error');
                // set last poped popover to be closed on click(see App.js => handlePopovers function)     
                App.setLastPopedPopover(input);
                input.popover('show');
                e.stopPropagation(); // prevent closing the popover

                return;
            }

            var btn = $(this);

            btn.attr('disabled', true);

            input.attr("readonly", true).
                attr("disabled", true).
                addClass("spinner");

            $.post('demo/username_checker.php', { username: input.val() }, function (res) {
                btn.attr('disabled', false);

                input.attr("readonly", false).
                    attr("disabled", false).
                    removeClass("spinner");

                input.popover('destroy');
                input.popover({
                    'placement': App.isRTL() ? 'left' : 'right',
                    'html': true,
                    'title': 'Username Availability',
                    'content': res.message,
                });

                // change popover font color based on the result
                if (res.status == 'OK') {
                    input.data('popover').tip().addClass('success');
                } else {
                    input.data('popover').tip().addClass('error');
                }

                // set last poped popover to be closed on click(see App.js => handlePopovers function)     
                App.setLastPopedPopover(input);

                input.popover('show');

            }, 'json');

        });
    }

    var handleUsernameAvailabilityChecker2 = function () {
        $("#username2_input").change(function () {
            var input = $(this);

            if (input.val() === "") {
                return;
            }

            input.attr("readonly", true).
                attr("disabled", true).
                addClass("spinner");

            $.post('demo/username_checker.php', { username: input.val() }, function (res) {
                input.attr("readonly", false).
                    attr("disabled", false).
                    removeClass("spinner");

                input.popover('destroy');
                input.popover({
                    'html': true,
                    'placement': App.isRTL() ? 'left' : 'right',
                    'title': 'Username Availability',
                    'content': res.message,
                });

                // change popover font color based on the result
                if (res.status == 'OK') {
                    input.data('popover').tip().addClass('success');
                } else {
                    input.data('popover').tip().addClass('error');
                }

                // set last poped popover to be closed on click(see App.js => handlePopovers function)     
                App.setLastPopedPopover(input);

                input.popover('show');

            }, 'json');

        });
    }

    var handleUsernameAvailabilityChecker3 = function () {
        $("#username3_input").change(function () {
            var input = $(this);

            if (input.val() === "") {
                return;
            }

            input.attr("readonly", true).
                attr("disabled", true).
                addClass("spinner");

            $.post('demo/username_checker.php', { username: input.val() }, function (res) {
                input.attr("readonly", false).
                    attr("disabled", false).
                    removeClass("spinner");

                input.popover('destroy');
                input.popover({
                    'html': true,
                    'placement': App.isRTL() ? 'left' : 'right',
                    'title': 'Username Availability',
                    'content': res.message,
                });

                // change popover font color based on the result
                if (res.status == 'OK') {
                    input.closest('.control-group').removeClass('error').addClass('success');
                    input.after('<span class="help-inline ok"></span>');
                } else {
                    input.closest('.control-group').removeClass('success').addClass('error');
                    $('.help-inline.ok', input.closest('.control-group')).remove();
                }

                // set last poped popover to be closed on click(see App.js => handlePopovers function)     
                App.setLastPopedPopover(input);

                input.popover('show');

            }, 'json');

        });
    }

    return {
        //main function to initiate the module
        init: function () {
            handleWysihtml5();
            handleToggleButtons();
            handleTagsInput();
            handleDatePickers();
            handleTimePickers();
            handleDatetimePicker();
            handleDateRangePickers();
            handleClockfaceTimePickers();
            handleColorPicker();
            handleSelect2();
            handleSelect2Modal();
            handleInputMasks();
            handleMultiSelect();
            handlePasswordStrengthChecker();
            handleUsernameAvailabilityChecker1();
            handleUsernameAvailabilityChecker2();
            handleUsernameAvailabilityChecker3();
        }

    };

}();