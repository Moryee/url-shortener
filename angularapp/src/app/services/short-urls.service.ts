import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { ShortUrl } from '../models/short-url.model';
import { Observable, of } from 'rxjs';
import { ShortUrlUpdate } from '../models/short-url-update.model';
import { ShortUrlCreate } from '../models/short-url-create.model';

@Injectable({
  providedIn: 'root'
})
export class ShortUrlsService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getShortUrls(): Observable<ShortUrl[]> {
    return this.http.get<ShortUrl[]>(this.apiUrl + '/api/ShortUrls');
  }

  getShortUrl(id: string): Observable<ShortUrl> {
    return this.http.get<ShortUrl>(this.apiUrl + '/api/ShortUrls/' + id);
  }

  createShortUrl(shortUrl: ShortUrlCreate): Observable<ShortUrl> {
    return this.http.post<ShortUrl>(this.apiUrl + '/api/ShortUrls', shortUrl);
  }

  updateShortUrl(id: string, shortUrl: ShortUrlUpdate): Observable<ShortUrl> {
    return this.http.put<ShortUrl>(this.apiUrl + '/api/ShortUrls/' + id, shortUrl);
  }

  deleteShortUrl(id: string): Observable<ShortUrl> {
    return this.http.delete<ShortUrl>(this.apiUrl + '/api/ShortUrls/' + id);
  }

  getShortUrlByShortUrl(url: string): Observable<ShortUrl> {
    return this.http.get<ShortUrl>(this.apiUrl + '/api/ShortUrls/Short/' + url);
  }
}
