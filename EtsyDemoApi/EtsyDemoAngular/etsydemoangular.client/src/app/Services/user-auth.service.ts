import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {
  private loggedIn = new BehaviorSubject<boolean>(this.hasToken());
  private usernameSource = new BehaviorSubject<string | null>(localStorage.getItem('username') || null);

  isLoggedIn$ = this.loggedIn.asObservable();
  username$ = this.usernameSource.asObservable();

  constructor() { }

  private hasToken(): boolean {
    return !!localStorage.getItem('token');
  }

  login(token: string, username: string): void {
    localStorage.setItem('token', token);
    localStorage.setItem('username', username);
    this.loggedIn.next(true);
    this.usernameSource.next(username);
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    this.loggedIn.next(false);
    this.usernameSource.next(null);
  }

  // Este método ya no es necesario, ya que ahora manejas el username directamente con BehaviorSubject
  getUsername(): string {
    return localStorage.getItem('username') || 'Invitado';
  }
}
