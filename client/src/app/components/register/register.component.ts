import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { RegisterUser } from "src/app/_models/user";
import { AccountService } from "src/app/_services/account.service";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"],
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerModel: RegisterUser = new RegisterUser();

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  register() {
    this.accountService.register(this.registerModel).subscribe({
      next: () => {
        this.cancel();
      },
      error: (error) => console.log(error),
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
