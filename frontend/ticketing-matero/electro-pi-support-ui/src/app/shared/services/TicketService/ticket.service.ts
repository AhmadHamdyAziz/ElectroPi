import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { PaginationResponse, TicketListItem, TicketDetails, CreateTicketRequest, TicketComment, AddTicketCommentRequest } from '../../models/Ticket.models';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    `${environment.apiUrl}/api/tickets`;

  getTickets(
    pageNumber: number,
    pageSize: number
  ): Observable<PaginationResponse<TicketListItem>> {

    const params = new HttpParams()
      .set('Pagination.PageNumber', pageNumber)
      .set('Pagination.PageSize', pageSize);

    return this.http.get<PaginationResponse<TicketListItem>>(
      this.apiUrl,
      { params }
    );
  }

  getTicket(id: string): Observable<TicketDetails> {
    return this.http.get<TicketDetails>(
      `${this.apiUrl}/${id}`
    );
  }

  createTicket(request: CreateTicketRequest): Observable<string> {
    return this.http.post<string>(
      this.apiUrl,
      request
    );
  }

  getComments(ticketId: string,
         pageNumber: number,
        pageSize: number
    ): Observable<PaginationResponse<TicketComment>> {
    const params = new HttpParams()
      .set('Pagination.PageNumber', pageNumber)
      .set('Pagination.PageSize', pageSize);

    return this.http.get<PaginationResponse<TicketComment>>(
      `${this.apiUrl}/${ticketId}/comments`,
      { params }
    );
  }

  addComment(
  ticketId: string,
  request: AddTicketCommentRequest
  ): Observable<string> {
    return this.http.post<string>(
      `${this.apiUrl}/${ticketId}/comments`,
      request
    );
  }
}