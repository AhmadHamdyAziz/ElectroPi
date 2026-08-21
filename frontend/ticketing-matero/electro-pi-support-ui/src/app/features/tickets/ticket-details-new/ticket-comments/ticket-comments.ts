import { DatePipe } from '@angular/common';
import {
  Component,
  effect,
  inject,
  input,
  signal
} from '@angular/core';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {
  MatPaginator,
  PageEvent
} from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';

import {
  TicketComment
} from '../../../../shared/models/Ticket.models';
import { PaginationResponse } from "@shared/models/PaginationResponse";

import { TicketService } from '../../../../shared/services/TicketService/ticket.service';
import { MatCard, MatCardHeader, MatCardTitle, MatCardContent } from "@angular/material/card";

@Component({
  selector: 'app-ticket-comments',
  imports: [
    DatePipe,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatPaginator,
    MatCard,
    MatCardHeader,
    MatCardTitle,
    MatCardContent
],
  templateUrl: './ticket-comments.html',
  styleUrl: './ticket-comments.scss',
})
export class TicketCommentsComponent {

  private readonly ticketService = inject(TicketService);

  readonly ticketId = input.required<string>();

  readonly comments =
    signal<PaginationResponse<TicketComment> | null>(null);

  readonly loadingComments = signal(false);
  readonly commentText = signal('');
  readonly addingComment = signal(false);

  readonly pageIndex = signal(0);
  readonly pageSize = signal(10);

  private readonly loadCommentsEffect = effect(() => {
    const ticketId = this.ticketId();

    if (!ticketId) {
      return;
    }

    this.loadComments();
  });

  loadComments(): void {
    const ticketId = this.ticketId();

    this.loadingComments.set(true);

    this.ticketService
      .getComments(
        ticketId,
        this.pageIndex() + 1,
        this.pageSize()
      )
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

  addComment(): void {
    const content = this.commentText().trim();

    if (!content) {
      return;
    }

    this.addingComment.set(true);

    this.ticketService
      .addComment(this.ticketId(), {
        comment: content
      })
      .subscribe({
        next: () => {
          this.commentText.set('');
          this.addingComment.set(false);

          this.loadComments();
        },
        error: error => {
          console.error('Failed to add comment', error);
          this.addingComment.set(false);
        }
      });
  }

  onCommentsPageChange(event: PageEvent): void {
    this.pageIndex.set(event.pageIndex);
    this.pageSize.set(event.pageSize);

    this.loadComments();
  }
}