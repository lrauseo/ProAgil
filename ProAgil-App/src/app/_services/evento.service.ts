import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/Http';
import { Observable } from 'rxjs';
import { IEvento } from '../_modules/IEvento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  baseUrl = 'http://localhost:5000/api/eventos';
  constructor(private http: HttpClient) { }

  getAllEventos(): Observable<IEvento[]> {
    return this.http.get<IEvento[]>(this.baseUrl);
  }

  getEventoByTema(tema: string): Observable<IEvento[]> {
    return this.http.get<IEvento[]>(`${this.baseUrl}/getByTema/${tema}`);
  }

  getEventoById(id: number): Observable<IEvento[]> {
    return this.http.get<IEvento[]>(`${this.baseUrl}/${id}`);
  }
}
