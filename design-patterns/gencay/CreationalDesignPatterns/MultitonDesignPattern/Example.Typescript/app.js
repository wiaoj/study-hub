"use strict";
class MailService {
    constructor() {
        this._mail = "";
        this._userName = "";
        this._password = "";
        console.log("MailService instance created");
    }
    static getInstance(key) {
        if (!this.instances[key])
            this.instances[key] = new MailService();
        return this.instances[key];
    }
    get mail() {
        return this._mail;
    }
    set mail(value) {
        this._mail = value;
    }
    get userName() {
        return this._userName;
    }
    set userName(value) {
        this._userName = value;
    }
    get password() {
        return this._password;
    }
    set password(value) {
        this._password = value;
    }
}
MailService.instances = {};
const gmail = MailService.getInstance("gmail");
const hotmail = MailService.getInstance("hotmail");
const yandex = MailService.getInstance("yandex");
gmail.mail = "...";
gmail.password = "...";
gmail.userName = "...";
hotmail.mail = "...";
hotmail.password = "...";
hotmail.userName = "...";
yandex.mail = "...";
yandex.password = "...";
yandex.userName = "...";
const gmail2 = MailService.getInstance("gmail");
const hotmail2 = MailService.getInstance("hotmail");
const yandex2 = MailService.getInstance("yandex");
//# sourceMappingURL=app.js.map