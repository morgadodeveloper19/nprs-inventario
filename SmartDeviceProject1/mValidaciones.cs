using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SmartDeviceProject1
{
    class mValidaciones
    {
        public int validarFormatoDatos(string u, string c)
        {
            int uLength = u.Length, cLength = c.Length, cInt = 0, cUpper = 0, res = 0;
            bool uLengthB = false, cLengthB = false, uArrayInt = false, cArrayInt = false;
            //Longitud de usuario
            if (uLength > 0)
            {

            }
            else
            {
                uLengthB = true;
            }
            //longitud de contraseña
            if (cLength >= 6 && cLength <= 15)
            {

            }
            else
            {
                cLengthB = true;
            }
            char[] uArray = u.ToCharArray(), cArray = c.ToCharArray();
            //Formato de usuario
            for (int pos = 0; pos < uArray.Length; pos++)
            {
                //Sin numeros en el usuario
                if (char.IsDigit(uArray[pos]))
                {
                    uArrayInt = true;
                    break;
                }
                else
                {
                }
            }
            //Formato de contraseña
            for (int pos = 0; pos < cArray.Length; pos++)
            {
                //contar el minimo de numeros
                if (char.IsDigit(cArray[pos]))
                {
                    cInt++;
                }
                else
                {
                }
                //contar las mayusculas
                if (char.IsUpper(cArray[pos]))
                {
                    cUpper++;
                }
                else
                {
                }
            }
            //validar los formatos
            if (cInt >= 2 && cUpper >= 1)
            {
            }
            else
            {
                cArrayInt = true;
            }
            res = Banderas(uLengthB, cLengthB, uArrayInt, cArrayInt);
            return res;
        }

        int Banderas(bool uLengthB, bool cLengthB, bool uArrayInt, bool cArrayInt)
        {
            int res = 0;
            if (uLengthB && cLengthB && uArrayInt && cArrayInt)
                res = 14; // se cumplen todas las condiciones
            if (!uLengthB && cLengthB && uArrayInt && cArrayInt)
                res = 13; // SOLO la longitud del usuario no cumple con el minimo de longitud
            if (uLengthB && !cLengthB && uArrayInt && cArrayInt)
                res = 12; // SOLO la longitud de la contrasaeña no cumple con el minimo de longitud
            if (uLengthB && cLengthB && !uArrayInt && cArrayInt)
                res = 11; // SOLO el usuario no cumple con el formato
            if (uLengthB && cLengthB && uArrayInt && !cArrayInt)
                res = 10; // SOLO la contraseña no cumple con el formato
            if (!uLengthB && !cLengthB && uArrayInt && cArrayInt)
                res = 9; // El usuario y la contraseña no cumplen con la longitud minimo
            if (!uLengthB && cLengthB && !uArrayInt && cArrayInt)
                res = 8; // El usuario no cumple con el minimo ni con el formato
            if (!uLengthB && cLengthB && uArrayInt && !cArrayInt)
                res = 7; // El usuario no cumple con la longitud minima y la contraseña no cumple con el formato
            if (uLengthB && !cLengthB && !uArrayInt && cArrayInt)
                res = 6; // La contraseña no cumple con la longitud minima y el usuario no cumple con el formato
            if (uLengthB && !cLengthB && uArrayInt && !cArrayInt)
                res = 5; // La contraseña no cumple con la longitud minima ni con el formato
            if (uLengthB && cLengthB && !uArrayInt && !cArrayInt)
                res = 4; //Los campos no cumplen con el formato requerido
            if (!uLengthB && !cLengthB && !uArrayInt && cArrayInt)
                res = 3; // SOLO la contraseña cumple con el formato requerido
            if (!uLengthB && !cLengthB && uArrayInt && !cArrayInt)
                res = 2; // SOLO el usuario cumple con el formato requerido
            if (uLengthB && !cLengthB && !uArrayInt && !cArrayInt)
                res = 1; // SOLO el usuario cumple con la longitud requerida
            if (!uLengthB && !cLengthB && !uArrayInt && !cArrayInt)
                res = 0; // NADA cumple con los requerimientos
            return res;
        }

        public void formatOneLabel(Label x, TextBox uT, TextBox cT)
        {
            x.ForeColor = Color.Red;
            uT.Text = "";
            cT.Text = "";
            uT.Focus();
        }

        public void formatTwoLabels(Label uL, Label cL, TextBox uT, TextBox cT)
        {
            uL.ForeColor = Color.Red;
            cL.ForeColor = Color.Red;
            uT.Text = "";
            cT.Text = "";
            uT.Focus();
        }
    }
}
