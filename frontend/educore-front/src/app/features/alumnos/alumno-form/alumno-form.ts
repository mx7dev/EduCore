import { Component, EventEmitter, inject, Input, Output, signal, model } from '@angular/core';
import { form, FormField, submit, required, minLength } from '@angular/forms/signals';
import { FormsModule } from '@angular/forms';
import { AlumnoService, CrearAlumnoDto } from '../../../core/services/alumno';
import { Button } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { InputText } from 'primeng/inputtext';
import { DatePicker } from 'primeng/datepicker';

@Component({
  selector: 'app-alumno-form',
  imports: [Button, Dialog, InputText, FormField, DatePicker, FormsModule],
  templateUrl: './alumno-form.html',
  styleUrl: './alumno-form.scss'
})
export class AlumnoForm {
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() alumnoGuardado = new EventEmitter<void>();

  private alumnoService = inject(AlumnoService);
  guardando = false;

  // Para el datepicker usamos model()
  fechaNacimiento = model<Date | null>(null);

  modelo = signal({
    dni: '',
    nombre: '',
    apellidoPaterno: '',
    apellidoMaterno: '',
    numeroCelular: '',
    correoPersonal: '',
    direccion: ''
  });

  formulario = form(this.modelo, (path) => {
    required(path.dni, { message: 'El DNI es obligatorio' });
    minLength(path.dni, 8, { message: 'El DNI debe tener 8 dígitos' });
    required(path.nombre, { message: 'El nombre es obligatorio' });
    required(path.apellidoPaterno, { message: 'El apellido paterno es obligatorio' });
  });

  cerrar() {
    this.visible = false;
    this.visibleChange.emit(false);
  }

  guardar() {
    submit(this.formulario, async () => {
      this.guardando = true;
      const v = this.modelo();
      const dto: CrearAlumnoDto = {
        dni: v.dni,
        nombre: v.nombre,
        apellidoPaterno: v.apellidoPaterno,
        apellidoMaterno: v.apellidoMaterno || undefined,
        fechaNacimiento: this.fechaNacimiento() 
          ? this.fechaNacimiento()!.toISOString().split('T')[0] 
          : '',
        numeroCelular: v.numeroCelular || undefined,
        correoPersonal: v.correoPersonal || undefined,
        direccion: v.direccion || undefined
      };

      await this.alumnoService.crear(dto).toPromise();
      this.guardando = false;
      this.alumnoGuardado.emit();
      this.cerrar();
    });
  }
}