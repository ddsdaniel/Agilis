interface Date {
  getOnlyDate(this: Date): Date;
  getFirstDayOfMonth(this: Date): Date;
  getLastDayOfMonth(this: Date): Date;
  addMonths(this: Date, months: number): Date;
  addDays(this: Date, months: number): Date;
}

Date.prototype.getOnlyDate = function (this: Date): Date {
  const newDate = new Date(this);
  newDate.setHours(0);
  newDate.setMinutes(0);
  newDate.setSeconds(0);
  newDate.setMilliseconds(0);
  return newDate;
};

Date.prototype.getFirstDayOfMonth = function (this: Date): Date {
  const newDate = new Date(this).getOnlyDate();
  newDate.setDate(1);
  return newDate;
};

Date.prototype.getLastDayOfMonth = function (this: Date): Date {
  const newDate = new Date(this).getFirstDayOfMonth().addMonths(1).addDays(-1);
  return newDate;
};

Date.prototype.addMonths = function (this: Date, months: number): Date {
  const newDate = new Date(this);
  newDate.setMonth(newDate.getMonth() + months);
  return newDate;
};

Date.prototype.addDays = function (this: Date, days: number): Date {
  const newDate = new Date(this);
  newDate.setDate(newDate.getDate() + days);
  return newDate;
};
