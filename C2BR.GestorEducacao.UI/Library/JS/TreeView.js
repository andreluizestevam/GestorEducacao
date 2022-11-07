//==================================================================
//Objetivo: Marcar e desmarcar o checkBox dos filhos e Pais
//==================================================================
function OnTreeClick(evt) 
{
    var src = window.event != window.undefined ? window.event.srcElement : evt.target;
    var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
    if (isChkBoxClick) {
        var parentTable = GetParentByTagName("table", src);
        var nxtSibling = parentTable.nextSibling;
        if (nxtSibling && nxtSibling.nodeType == 1) {
            //Node Children
            if (nxtSibling.tagName.toLowerCase() == "div")
            {
                //Marca e desmarca os filhos(Children)
                CheckUncheckChildren(parentTable.nextSibling, src.checked);
            }
        }
        //Marca ou desmarque os pais de todos os níveis
        CheckUncheckParents(src, src.checked);
    }
}

function CheckUncheckChildren(childContainer, check) 
{
    //Marcar e desmarcar o checkBox dos filhos
    var chkChildBoxes = childContainer.getElementsByTagName("input");
    var ChkchildBoxCount = chkChildBoxes.length;
    for (var i = 0; i < ChkchildBoxCount; i++) {
        chkChildBoxes[i].checked = check;
    }
}

function CheckUncheckParents(srcChild, check) 
{
    //Marcar e desmarcar o checkBox dos pais de todos os níveis
    var parentDiv = GetParentByTagName("div", srcChild);
    var parentNodeTable = parentDiv.previousSibling;

    if (parentNodeTable) {
        var checkUncheckSwitch;
        if (check) //Verifica se o checkbox está marcado
        {
            var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
            checkUncheckSwitch = true;

            var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
            if (inpElemsInParentTable.length > 0) {
                var parentNodeChkBox = inpElemsInParentTable[0];
                parentNodeChkBox.checked = checkUncheckSwitch;
                //Recursividade
                CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
            }
        }
    }
}

function AreAllSiblingsChecked(chkBox) 
{
    var parentDiv = GetParentByTagName("div", chkBox);
    var childCount = parentDiv.childNodes.length;
    for (var i = 0; i < childCount; i++) {
        //Verifica se o nó filho é um nó de elemento
        if (parentDiv.childNodes[i].nodeType == 1) {
            if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                //Se algum dos irmãos nós não estão marcados, return false
                if (!prevChkBox.checked) {
                    return false;
                }
            }
        }
    }
    return true;
}

function GetParentByTagName(parentTagName, childElementObj) 
{
    //Função utilizada para obter o conteúdo de um elemento por tagname
    var parent = childElementObj.parentNode;
    while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
        parent = parent.parentNode;
    }
    return parent;
}

//===================================================================================================
function ClicouSetas(evt, tvNome) {
    evt = getEvent(evt);
    var tecla = getKeyCode(evt);
    if (document.activeElement == null)
        return true;
    if (document.activeElement.tagName.toLowerCase() != 'a') //se não for o link
        return true;
    if (tecla != 37 && tecla != 38 && tecla != 39 && tecla != 40 && tecla != 13) //se as teclas não forem as SETAS e não for o ENTER sai do método
        return true;

    //TreeView_ToggleNode
    if (tecla == 13)//enter
        acionaItem(document.activeElement);
    else if (tecla == 39)//direita
    {
        var proximoItem = document.activeElement;
        var div_atual = proximoItem.parentNode.parentNode.parentNode.parentNode.parentNode.id;
        proximoItem = document.getElementById(tvNome + 't' + (parseInt(proximoItem.id.replace(tvNome + 't', '')) + 1).toString());
        if (proximoItem) {
            if (document.activeElement.href.indexOf('focus') != -1 || document.activeElement.href.indexOf('TreeView_ToggleNode') != -1)//se existe a função para expandir
            {
                if (proximoItem.parentNode.parentNode.parentNode.parentNode.parentNode.tagName.toLowerCase() == 'div'
                    && proximoItem.parentNode.parentNode.parentNode.parentNode.parentNode.style.display != 'none'
                    && proximoItem.parentNode.parentNode.parentNode.parentNode.parentNode.id != div_atual) {
                    proximoItem.focus();
                    //document.getElementById('ultimoFoco').value = proximoItem.id;
                }
                else
                    acionaItem(document.activeElement);
            }
        }
    }
    else if (tecla == 37)//esquerda
    {
        var proximoItem = document.activeElement;
        var div_atual = proximoItem;
        while (div_atual.tagName.toLowerCase() != 'div')
            div_atual = div_atual.parentNode; //busca o div atual
        div_atual = div_atual.parentNode;
        while (div_atual.tagName.toLowerCase() != 'div')
            div_atual = div_atual.parentNode; //busca o div do pai
        if (proximoItem.href.indexOf('focus') != -1 || proximoItem.href.indexOf('TreeView_ToggleNode') != -1)//se o nó está expandido fechar
        {
            var dvp = proximoItem.href.split(',')[proximoItem.href.split(',').length - 1].replace('\'', '').replace('\'', '').replace('document.getElementById(', '').replace(')', '').replace(')', '');
            var proximo_div = document.getElementById(dvp); //qual o div que ele aciona
            if (proximo_div && proximo_div.style.display != 'none') {
                acionaItem(document.activeElement);
                return false;
            }
        }

        while (proximoItem != null) {
            proximoItem = document.getElementById(tvNome + 't' + (parseInt(proximoItem.id.replace(tvNome + 't', '')) - 1).toString());
            if (proximoItem
                && proximoItem.parentNode.parentNode.parentNode.parentNode.parentNode.style.display != 'none'
                && proximoItem.parentNode.parentNode.parentNode.parentNode.parentNode.id == div_atual.id) {
                try {
                    proximoItem.focus();
                    //document.getElementById('ultimoFoco').value = proximoItem.id;
                    break;
                } catch (ex) { continue; }
            }
        }
    }
    else if (tecla == 40)//baixo
    {
        var proximoItem = document.activeElement;
        while (proximoItem != null) {
            proximoItem = document.getElementById(tvNome + 't' + (parseInt(proximoItem.id.replace(tvNome + 't', '')) + 1).toString());
            if (proximoItem && proximoItem.parentNode.parentNode.parentNode.parentNode.parentNode.tagName.toLowerCase() == 'div' && proximoItem.parentNode.parentNode.parentNode.parentNode.parentNode.style.display != 'none') {
                try {
                    proximoItem.focus();
                    //document.getElementById('ultimoFoco').value = proximoItem.id;
                    break;
                } catch (ex) { continue; }
            }
        }
    }
    else if (tecla == 38)//cima
    {
        var proximoItem = document.activeElement;
        while (proximoItem != null) {
            proximoItem = document.getElementById(tvNome + 't' + (parseInt(proximoItem.id.replace(tvNome + 't', '')) - 1).toString());
            if (proximoItem && proximoItem.parentNode.parentNode.parentNode.parentNode.parentNode.tagName.toLowerCase() == 'div' && proximoItem.parentNode.parentNode.parentNode.parentNode.parentNode.style.display != 'none') {
                try {
                    proximoItem.focus();
                    //document.getElementById('ultimoFoco').value = proximoItem.id;
                    break;
                } catch (ex) { continue; }
            }
        }
    }
    return false;
}
function acionaItem(item) {
    item.setAttribute('onclick', item.href);
    if (typeof (item.onclick) == 'function')
        item.onclick();
    else
        item.click();
    item.setAttribute('onclick', '');
    //document.getElementById('ultimoFoco').value = item.id;
}

//Recupera o código da tecla que foi pressionado
function getKeyCode(evt) {
    var code;
    if (typeof (evt.keyCode) == 'number')
        code = evt.keyCode;
    else if (typeof (evt.which) == 'number')
        code = evt.which;
    else if (typeof (evt.charCode) == 'number')
        code = evt.charCode;
    else
        return 0;

    return code;
}
// recupera o evento do form
function getEvent(evt) {
    if (!evt) evt = window.event; //IE
    return evt;
}
