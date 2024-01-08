import * as d3 from 'd3';

// load the data from a json file and create the d3 svg in the then function
d3.json('data.json').then((data) => {

   //create the svg
    const svg = d3.select('body')
        .append('svg')
        .attr('width', 500)
        .attr('height', 500);

    // create the scales for the x and y axis
    const xScale = d3.scaleBand()
        .domain(data.map((d: any) => d.month))
        .range([0, 500]);

    const yScale = d3.scaleLinear()
        .domain([0, d3.max(data, (d: any) => d.albumsSold)])
        .range([500, 0]);

    // create axes for the x and y axis
    const xAxis = d3.axisBottom(xScale);
    const yAxis = d3.axisLeft(yScale);

    // append the x and y axis to the svg
    svg.append('g')
        .attr('transform', 'translate(0, 500)')
        .call(xAxis);

    svg.append('g')
        .call(yAxis);

    // generate a line chart based on the albums sales data
    const line = d3.line()
        .x((d: any) => xScale(d.month))
        .y((d: any) => yScale(d.albumsSold));

    svg.append('path')
        .datum(data)
        .attr('fill', 'none')
        .attr('stroke', 'steelblue')
        .attr('stroke-width', 2)
        .attr('d', line);
});


