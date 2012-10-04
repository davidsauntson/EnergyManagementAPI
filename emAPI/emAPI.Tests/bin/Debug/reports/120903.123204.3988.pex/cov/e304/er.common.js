// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==

//-----------------------------------------------------
// Clipboard
//-----------------------------------------------------
function copyValueToClipboard(id)
{
    span = document.getElementById(id);
    if(span==null)
    	return;
    window.clipboardData.setData("Text", span.innerText);
}

function copyValueFromClipboard(id)
{
    input = document.getElementById(id);
    if(input==null)
    	return;
    input.value = window.clipboardData.getData("Text");
}

function copySourceToClipboardFromId(id)
{
    var element = document.getElementById(id);
    if(element==null)
    	return;
	window.clipboardData.setData("Text", element.outerHTML);
}

function copySourceToClipboard()
{
	var fullSource = document.documentElement.outerHTML;
	window.clipboardData.setData("Text", fullSource);
}

function swapImage(img)
{
    var temp;
    
    temp = img.srctemp;
    img.srctemp = img.src;
    img.src = temp;
    
    temp = img.widthtemp;
    img.widthtemp = img.width;
    img.width = temp;
    
    temp = img.heighttemp;
    img.heighttemp = img.height;
    img.height = temp;
}


// ----------------------------------------------------
// Hides a div
// ----------------------------------------------------
function show(id)
{
   showDiv(document.getElementById(id));
}

function showDiv(div)
{
   if (div!=null)
   {
       div.style.display = "block";
   }
}

// ----------------------------------------------------
// Hides a div
// ----------------------------------------------------
function hide(id)
{
   hideDiv(document.getElementById(id));
}

function hideDiv(div)
{
   if (div!=null)
   {
       div.style.display = "none";
   }
}

// ----------------------------------------------------
// Show a div in front of a target
// ----------------------------------------------------
function show(id, target)
{
   div = document.getElementById(id);
   if (div!=null)
   {
     div.style.pixelLeft = getAbsoluteLeft(target);
     div.style.pixelTop = getAbsoluteTop(target);   
     showDiv(div);
   }
}

// ----------------------------------------------------
// Div togling
// ----------------------------------------------------
function toggle(toggleId, divId)
{
    tog = document.getElementById(toggleId);
    div = document.getElementById(divId);
    
    if(tog==null)
        return;
        
    if(div==null)
        return;

    if (tog.innerText=="[+]")
    {
        showDiv(div);
        tog.innerText = "[-]";
    }
    else
    {
        hideDiv(div);
        tog.innerText = "[+]";
    }
}

function toggleDiv(divId)
{
    div = document.getElementById(divId);
    if (div==null) return;
    if (div.style.display=="block")
        hideDiv(div);
    else
        showDiv(div);
}

// go to and show
function goAndShow(id)
{
   target = document.getElementById(id);
   if(target != null)
   {
       // make sure parents are not hidden
       current = target;
       while(current !=null)
       {
           showDiv(current);
           current = current.parentElement;
       }
       
       // goto
       target.scrollIntoView();
   }
}

// ----------------------------------------------------
// a bunch of things to do on load
// ----------------------------------------------------
function bodyOnLoad()
{
    // did the user request to go somewhere?
    var url = document.URL;
    var pound = url.lastIndexOf('#');
    if (pound > -1)
    {
        var id = url.substring(pound + 1);
        goAndShow(id);
    }
}

// ----------------------------------------------------
// Get the absolute left position of el
// ----------------------------------------------------
function getAbsoluteLeft(el) 
{
    var x = 0;
    if (el && typeof el.offsetParent != "undefined") 
    {
        while (el && typeof el.offsetLeft == "number") 
        {
            x += el.offsetLeft;
            el = el.offsetParent;
        }
     }
    return x;
}

// ----------------------------------------------------
// Get the absolute top position of el
// ----------------------------------------------------
function getAbsoluteTop(el) 
{
    var y = 0;
    if (el && typeof el.offsetParent != "undefined") 
    {
        while (el && typeof el.offsetTop == "number") 
        {
            y += el.offsetTop;
            el = el.offsetParent;
        }
    }
    return y;
}

// ----------------------------------------------------
// strings
// ----------------------------------------------------
function isNullOrEmpty(string)
{
    return string == null || string.length == 0;
}

// ------------------------------------------------------------------
// Reverts the content of a pre section
// ------------------------------------------------------------------
function revertPreInnerText(id)
{
   var node = document.getElementById(id);
   var text = revertLines(node.innerText);
   node.innerHTML = '<pre>'+text+'</pre>';
}

// ------------------------------------------------------------------
// Splits a string and reverts lines
// ------------------------------------------------------------------
function revertLines(text)
{
   if (isNullOrEmpty(text))
       return text;
       
   var lines = text.split('\n');
   var result = '';
   for(var i = lines.length - 1; i >= 0;i--)
   {
      result += lines[i] + '\n';
   }
   return result;
}

// ------------------------------------------------------------------
// Regex
// ------------------------------------------------------------------
function initRegexes()
{    
    // there's not escape method for regular expressions in jscript!
    // we are precomputing and stuffing it here
    RegExp.escape = escapeRegex;
}
 
function escapeRegex(text) 
{
    if (!arguments.callee.sRE) 
    {
        var specials = ['/', '.', '*', '+', '?', '|','(', ')', '[', ']', '{', '}', '\\'];
        arguments.callee.sRE = new RegExp('(\\' + specials.join('|\\') + ')', 'g');
    }
    return text.replace(arguments.callee.sRE, '\\$1');
}


// ------------------------------------------------------------------
// redirect
// ------------------------------------------------------------------
function openNewWindow(url)
{
    window.open(url, '_blank');
}

// ------------------------------------------------------------------
// table
// ------------------------------------------------------------------
// update style of all rows to be displayed
function showAllRows(tableid)
{
    var table = document.all[tableid];
    if (table == null) 
        return;
    for(var i = 0; i < table.rows.length; i++)
        table.rows[i].style.display = "inline";
}

// reorders rows based on column value
function sortTableByColumn(tableid, keyColumnIndex, rowsToSkip)
{
    var table = document.all[tableid];
    if (table == null) 
        return;
    var ascending = table.ascending == null;
    table.ascending = (ascending) ? true : null;

    // copy rows into array    
    var rows = new Array(table.rows.length - rowsToSkip);
    for(var i = 0; i < rows.length; ++i)
    {
        var index = rowsToSkip + i;
        rows[i] = new Object();
        rows[i].row = table.rows[index];
        rows[i].columnIndex = keyColumnIndex;
    }
    // sort and update
    if (ascending)
        rows = rows.sort(compareAscendingRows);
    else
        rows = rows.sort(compareDescendingRows);
    
    // update table
    for(var i = 0; i< rows.length;++i)
        table.moveRow(rows[i].row.rowIndex, rowsToSkip + i);
}

// compare rows
function compareAscendingRows(left, right)
{
    var leftText = left.row.cells[left.columnIndex].innerText;
    var rightText = right.row.cells[right.columnIndex].innerText;
    
    var c = leftText.localeCompare(rightText);
        
    return c;
}

function compareDescendingRows(left, right)
{
    return -compareAscendingRows(left, right);
}