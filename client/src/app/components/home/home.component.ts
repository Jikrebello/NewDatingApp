import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { User } from "src/app/_models/user";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: User[] = [];

  constructor(private https: HttpClient) {}

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  getUsers() {
    this.https.get("https://localhost:5001/api/users/GetAllUsers").subscribe({
      next: (response: any) => (this.users = response),
      error: (error) => console.log(error),
      complete: () => console.log("Request has completed."),
    });
  }
}
