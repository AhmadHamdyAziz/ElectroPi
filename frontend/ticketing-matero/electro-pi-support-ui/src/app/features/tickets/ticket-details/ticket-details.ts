import { DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';

import {
  TicketDetails as Ticket,
  TicketComment,
  TicketPriority,
  TicketState
} from '../../../shared/models/Ticket.models';
import { PaginationResponse } from "@shared/models/PaginationResponse";

import { TicketService } from '../../../shared/services/TicketService/ticket.service';

import { RouterLink } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { MatDivider } from "@angular/material/divider";

@Component({
  selector: 'app-ticket-details',
  imports: [
    DatePipe,
    MatCardModule,
    MatIconModule,
    RouterLink,
    MatFormFieldModule,
    MatInputModule,
    MatPaginator,
    MatDivider
],
  templateUrl: './ticket-details.html',
  styleUrl: './ticket-details.scss',
})
export class TicketDetails {

  private readonly route = inject(ActivatedRoute);
  private readonly ticketService = inject(TicketService);

  readonly ticket = signal<Ticket | null>(null);

readonly comments = signal<PaginationResponse<TicketComment> | null>(null);  readonly loadingComments = signal(false);
  readonly commentText = signal('');
  readonly addingComment = signal(false);

    pageIndex = 0;
    pageSize = 10;

  constructor() {
    const id = this.route.snapshot.paramMap.get('id');

    if (!id) {
      return;
    }

    this.ticketService.getTicket(id).subscribe({
      next: ticket => {
        this.ticket.set(ticket);
        this.loadComments(id);
      },
      error: error => {
        console.error('Failed to load ticket', error);
      }
    });
  }

  getStatusLabel(status: number): string {
    return TicketState[status] ?? 'Unknown';
  }

  getPriorityLabel(priority: number): string {
    return TicketPriority[priority] ?? 'Unknown';
  }

  getStatusClass(status: number): string {
    return `status-${TicketState[status]?.toLowerCase() ?? 'unknown'}`;
  }

  getPriorityClass(priority: number): string {
    return `priority-${TicketPriority[priority]?.toLowerCase() ?? 'unknown'}`;
  }

  addComment(): void {
  const ticket = this.ticket();

  if (!ticket) {
    return;
  }

  const content = this.commentText().trim();

  if (!content) {
    return;
  }

  this.addingComment.set(true);

  this.ticketService
    .addComment(ticket.id, {
      comment: content
    })
    .subscribe({
      next: () => {
        this.commentText.set('');
        this.addingComment.set(false);

        this.loadComments(ticket.id);
      },

      error: error => {
        console.error('Failed to add comment', error);
        this.addingComment.set(false);
      }
    });
  }

  private loadComments(ticketId: string): void {
        this.loadingComments.set(true);
  this.ticketService
    .getComments(ticketId, this.pageIndex + 1, this.pageSize)
    .subscribe({
      next: result => {
        this.comments.set(result);
        this.loadingComments.set(false);
      },
      error: error => {
        console.error('Failed to load comments:', error);
        this.loadingComments.set(false);
      }
    });
  }

  onCommentsPageChange(event: PageEvent): void {
    const ticket = this.ticket();

    if (!ticket) {
      return;
    }

    this.ticketService
      .getComments(
        ticket.id,
        event.pageIndex + 1,
        event.pageSize
      )
      .subscribe({
        next: result => this.comments.set(result),
        error: error => console.error('Failed to load comments:', error)
      });
  }
}