function createGrid() {
    var grid = "<div class='row-docs-grid'> " +
            "<div class='row show-grid'> " +
                "<div class='span1'>1</div> " +
                "<div class='span1'>1</div> " +
                "<div class='span1'>1</div> " +
                "<div class='span1'>1</div> " +
                "<div class='span2'>2</div> " +
                "<div class='span2'>2</div> " +
            "</div> " +
            "<div class='row show-grid'> " +
                "<div class='span4'>4</div> " +
                "<div class='span7'>7</div> " +
            "</div> " +
        "</div> ";
    return grid;
}

function createGridWhitParams(niveles, ventanas) {
    var grid = "<div class='row-docs-grid'>";
    for(i = 0; i < niveles ;i++){
        grid += "<div class='row show-grid'>";
        for (j = 0; j < ventanas; j++) {
            grid+= "<div class='span1' id='toolhere' data-toggle='tooltip' title='Nivel " +(niveles - i)+ " - Ventana " + (j+1) + "'>1</div>";
        }        
        grid += "</div>";
    }
    grid += "</div>";

    return grid;
}