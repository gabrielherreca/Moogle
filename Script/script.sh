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

}


slides() {
  
  cd Slides
  
pdflatex Slides.tex

}


show_report() {
  cd Report
  
if [ ! -f "Report.pdf" ]; then
  report
fi

if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "cygwin" ]]; then
  start Report.pdf
elif [[ "$OSTYPE" == "linux-gnu" ]]; then
  xdg-open Report.pdf

fi


}


show_slides() {
  cd Slides

if [ ! -f "Slides.pdf" ]; then
 
  slides
fi

if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "cygwin" ]]; then
  start Slides.pdf
elif [[ "$OSTYPE" == "linux-gnu" ]]; then
 
  xdg-open Slides.pdf

fi
   
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

while true;do

cd ..
clear
show_menu
done

exit 0

