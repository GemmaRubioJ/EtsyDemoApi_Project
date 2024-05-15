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

  login(token: string, username: string, id: string): void {
    localStorage.setItem('token', token);
    localStorage.setItem('username', username);
    localStorage.setItem('userId', id);
    this.loggedIn.next(true);
    this.usernameSource.next(username);
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    localStorage.removeItem('userId'); 
    this.loggedIn.next(false);
    this.usernameSource.next(null);
  }

  getUsername(): string {
    return localStorage.getItem('username') || 'Invitado';
  }

  isLoggedIn(): boolean {
    return this.loggedIn.getValue();
  }

  getUserId(): number | null {
    const userId = localStorage.getItem('userId');
    return userId ? parseInt(userId, 10) : null ;
  }
}
