import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = environment.apiBaseUrl;
  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    const url = `${this.baseUrl}/api/login`; // Replace with your actual endpoint
    const body = { username, password };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post(url, body, { headers });
  }
}
