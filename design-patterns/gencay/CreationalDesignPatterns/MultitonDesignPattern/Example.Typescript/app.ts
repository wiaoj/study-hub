class MailService {
    private static instances: { [key: string]: MailService } = {};
    private _mail: string = "";
    private _userName: string = "";
    private _password: string = "";

    private constructor() {
        console.log("MailService instance created");
    }

    public static getInstance(key: string): MailService {
        if (!this.instances[key]) this.instances[key] = new MailService();
         
        return this.instances[key];
    }
     
    public get mail(): string {
        return this._mail;
    }

    public set mail(value: string) {
        this._mail = value;
    }

    public get userName(): string {
        return this._userName;
    }

    public set userName(value: string) {
        this._userName = value;
    }

    public get password(): string {
        return this._password;
    }

    public set password(value: string) {
        this._password = value;
    } 
}

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