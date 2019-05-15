var GetNamebyEnum = function (Enum, Id) {
    //объявление массивов = enum
    var CurrencyEnum = ["Руб", "Долл", "Евро"];

    var DateTypes = ["День", "Неделя", "Месяц", "Год"];

    var FinanceTypes = ["Поступления", "Расход"];

    var ProjectStatus = ["Начало"];

    var FinancePlaceTypes = ["Наличные", "Карта", "Расчетный счет"];

    switch (Enum) {
        case "Currency":
            return CurrencyEnum[Id];
        case "DateTypes":
            return DateTypes[Id];
        case "ProjectStatus":
            return ProjectStatus[Id];
        case "FinanceTypes":
            return FinanceTypes[Id];
        case "FinancePlaceTypes":
            return FinancePlaceTypes[Id];
            
        default:
            return "Не найдено";
    }
};

