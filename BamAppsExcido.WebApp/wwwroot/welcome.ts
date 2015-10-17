export class Welcome {
    heading = 'Welcome to Excido!';

    firstName = 'Jo';
    lastName = 'Doe';



    get fullName() {
        return `${this.firstName} ${this.lastName}`;
    }

    submit() {
        alert(`Welcome, ${this.fullName}!`);
    }
}