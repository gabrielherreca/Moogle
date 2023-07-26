#!/bin/bash



run() {
 
  dotnet watch run --project MoogleServer 
  
}


clean() {
  cd Slides
for archivo in *
do
  if [[ $archivo != "Slides.tex" && $archivo != "matcom.jpg" ]]
  then
    rm "$archivo"
  fi
done
cd ..
cd Report
for archivo in *
do
  if [[ $archivo != "Report.tex" ]]
  then
    rm "$archivo"
  fi
done
  
}


report() {
   cd Report
  
pdflatex Report.tex
pdflatex Report.tex

}


slides() {

  
  cd Slides
  
pdflatex Slides.tex
pdflatex Slides.tex

}


show_report() {
  cd Report
  
if [ ! -f "Report.pdf" ]; then
cd ..
  report
fi

echo "¿Deseas usar la herramienta predeterminada para abrir el informe? (S/N)"
  read respuesta

  if [[ "$respuesta" == "S" || "$respuesta" == "s" ]]; then
    if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "cygwin" ]]; then
      start Report.pdf
    elif [[ "$OSTYPE" == "linux-gnu" ]]; then
      xdg-open Report.pdf
    fi
  elif [[ "$respuesta" == "N" || "$respuesta" == "n" ]]; then
    echo "Ingresa el comando de la herramienta que deseas utilizar:"
    read herramienta
    $herramienta Report.pdf
  else
    echo "Respuesta inválida. La herramienta predeterminada se utilizará para abrir el informe."
    if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "cygwin" ]]; then
      start Report.pdf
    elif [[ "$OSTYPE" == "linux-gnu" ]]; then
      xdg-open Report.pdf
    fi
  fi
}





show_slides() {
  cd Slides

if [ ! -f "Slides.pdf" ]; then
cd ..
  slides
fi

echo "¿Deseas usar la herramienta predeterminada para abrir el informe? (S/N)"
  read respuesta

  if [[ "$respuesta" == "S" || "$respuesta" == "s" ]]; then
    if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "cygwin" ]]; then
      start Slides.pdf
    elif [[ "$OSTYPE" == "linux-gnu" ]]; then
      xdg-open Slides.pdf
    fi
  elif [[ "$respuesta" == "N" || "$respuesta" == "n" ]]; then
    echo "Ingresa el comando de la herramienta que deseas utilizar:"
    read herramienta
    $herramienta Slides.pdf
  else
    echo "Respuesta inválida. La herramienta predeterminada se utilizará para abrir el informe."
    if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "cygwin" ]]; then
      start Slides.pdf
    elif [[ "$OSTYPE" == "linux-gnu" ]]; then
      xdg-open Slides.pdf
    fi
  fi
}


show_slides_c() {
  cd Slides

if [ ! -f "Slides.pdf" ]; then
cd ..
  slides
fi

if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "cygwin" ]]; then
    start Slides.pdf
    
elif [[ "$OSTYPE" == "linux-gnu" ]]; then
    xdg-open Slides.pdf



fi
   
}

show_report_c() {
  cd Report

if [ ! -f "Report.pdf" ]; then
cd ..
  report
fi

if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "cygwin" ]]; then
    start Report.pdf
    
elif [[ "$OSTYPE" == "linux-gnu" ]]; then
    xdg-open Report.pdf



fi
   
}
   



show_report_a() {
  cd Report
  
if [ ! -f "Report.pdf" ]; then
cd ..
  report
fi

"$viewer" Report.pdf

}


show_slides_a() {
  cd Slides

if [ ! -f "Slides.pdf" ]; then
cd ..
  slides
fi

"$viewer" Slides.pdf
   
}




show_menu() {

  echo "Seleccione una opción:"
  echo "1. Run"
  echo "2. Clean"
  echo "3. Report"
  echo "4. Slides"
  echo "5. Show Report"
  echo "6. Show Slides"
  echo "0. Exit"
  read -p "Ingrese el número de la opción deseada: " option

  case $option in
    1) run ;;
    2) clean ;;
    3) report ;;
    4) slides ;;
    5) show_report ;;
    6) show_slides ;;
    0) exit 0 ;;
    *) echo "Opción inválida"; show_menu ;;
  esac
}


if [ $# -eq 0 ]; then
  while true; do
    cd ..
    show_menu
  done


elif  [ $# -eq 1 ] || [ $# -eq 2 ]; then
  viewer=$2
  cd ..
  if [ $# -eq 1 ] ; then
  case $1 in
    
    "run") run ;;
    "clean") clean ;;
    "report") report ;;
    "slides") slides ;;
    "show_report") show_report_c ;;
    "show_slides") show_slides_c ;;
    *) echo "Opción inválida" ;;
  esac
  fi
  if [ $# -eq 2 ] ; then  
  case $1 in
   
    "run") run ;;
    "clean") clean ;;
    "report") report ;;
    "slides") slides ;;
    "show_report") show_report_a ;;
    "show_slides") show_slides_a ;;
    *) echo "Opción inválida" ;;
  esac
  fi
else
  echo "Cantidad inválida de argumentos"
fi 
exit 1






