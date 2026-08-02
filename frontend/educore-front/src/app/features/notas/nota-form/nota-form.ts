import { Component, EventEmitter, inject, Input, Output, signal } from '@angular/core';
import { form, FormField, required, submit } from '@angular/forms/signals';
import { NotaService, RegistrarNotaDto } from '../../../core/services/nota';
import { Button } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { InputText } from 'primeng/inputtext';
import { Toast } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-nota-form',
  imports: [Button, Dialog, InputText, FormField, Toast],
  providers: [MessageService],
  templateUrl: './nota-form.html',
  styleUrl: './nota-form.scss'
})
export class NotaForm {
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() notaGuardada = new EventEmitter<void>();

  private notaService = inject(NotaService);
  private messageService = inject(MessageService);
  guardando = false;

  modelo = signal({
    matriculaId: '',
    cursoId: '',
    bimestre: '',
    calificacion: '',
    comentario: ''
  });

  formulario = form(this.modelo, (path) => {
    required(path.matriculaId, { message: 'La matrícula es obligatoria' });
    required(path.cursoId, { message: 'El curso es obligatorio' });
    required(path.bimestre, { message: 'El bimestre es obligatorio' });
    required(path.calificacion, { message: 'La calificación es obligatoria' });
  });

 cerrar() {
  this.visible = false;
  this.visibleChange.emit(false);
  this.modelo.set({
    matriculaId: '',
    cursoId: '',
    bimestre: '',
    calificacion: '',
    comentario: ''
  });
}

  guardar() {
    submit(this.formulario, async () => {
      this.guardando = true;
      const v = this.modelo();
      const dto: RegistrarNotaDto = {
        matriculaId: Number(v.matriculaId),
        cursoId: Number(v.cursoId),
        bimestre: Number(v.bimestre),
        calificacion: Number(v.calificacion),
        comentario: v.comentario || undefined
      };

      this.notaService.registrar(dto).subscribe({
        next: (response: any) => {
          this.guardando = false;
          if (response.success) {
            this.messageService.add({ severity: 'success', summary: 'Éxito', detail: response.message });
            this.notaGuardada.emit();
            this.cerrar();
          } else {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: response.message });
          }
        },
        error: () => {
          this.guardando = false;
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Ocurrió un error inesperado' });
        }
      });
    });
  }
}