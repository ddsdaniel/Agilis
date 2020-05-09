import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  clear() {
    localStorage.clear();
  }
  removeItem(key: string) {
    localStorage.removeItem(key);
  }

  setString(key: string, data: string) {
    localStorage.setItem(key, data);
  }
  getString(key: string): string {
    return localStorage.getItem(key);
  }

  setNumber(key: string, data: number) {
    localStorage.setItem(key, data.toString());
  }
  getNumber(key: string): number {
    // + converte para number, uma vez que não sabemos se é inteiro ou fracionário
    return +localStorage.getItem(key);
  }

  setBooelan(key: string, data: boolean) {
    localStorage.setItem(key, data.toString());
  }
  getBooelan(key: string): boolean {
    return localStorage.getItem(key).toLowerCase() === 'true';
  }

  setDate(key: string, data: Date) {
    localStorage.setItem(key, data.toString());
  }
  getDate(key: string): Date {
    return new Date(Date.parse(localStorage.getItem(key)));
  }

  setJSON<T>(key: string, data: T) {
    localStorage.setItem(key, JSON.stringify(data));
  }
  getJson<T>(key: string): T {
    return JSON.parse(localStorage.getItem(key)) as T;
  }

}
