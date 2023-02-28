export class SignInViewModel {
	private _email!: string;
	private _password!: string;
	private _password_confirmation!: string;
	private _remember_me = false;

	public get remember_me(): boolean {
		return this._remember_me;
	}

	public set remember_me(value: boolean) {
		this._remember_me = value;
	}

	public get email(): string {
		return this._email;
	}
	public set email(value: string) {
		this._email = value;
	}

	public get password(): string {
		return this._password;
	}
	public set password(valeu: string) {
		this._password = valeu;
	}

	public get password_confirmation(): string {
		return this._password_confirmation;
	}

	public set password_confirmation(value: string) {
		this._password_confirmation = value;
	}
}
